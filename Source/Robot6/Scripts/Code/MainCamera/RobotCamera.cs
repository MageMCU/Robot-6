using UnityEngine;
using UnityEngine.InputSystem;

namespace MainCamera
{
    public class RobotCamera : MonoBehaviour
    {
        public static RobotCamera Instance;

        private Keyboard _keyboard;
        private Mouse _mouse;

        public Transform TargetLookAt;
        public float Distance = 10f;
        public float DistanceMin = 3f;
        public float DistanceMax = 15;

        public float DistanceSmooth = 0.3f;
        public float DistanceResumeSmooth = 0.3f;

        public float X_MouseSensitivity = 0.5f;
        public float Y_MouseSensitivity = 0.5f;
        public float MouseWheelSensitivity = 5f;

        public float X_Smooth = 0.025f;
        public float Y_Smooth = 0.025f;

        public float Y_MinLimit = 5f;             // angle
        public float Y_MaxLimit = 85f;               // angle

        public float OcclusionDistanceStep = 0.5f;
        public int MaxOcclusionChecks = 10;

        private float mouseX = 0f;
        private float mouseY = 0f;

        private float velocityX = 0f;
        private float velocityY = 0f;
        private float velocityZ = 0f;
        private float velocityDistance = 0f;

        private float startDistance = 0f;
        private Vector3 position = Vector3.zero;
        private Vector3 desiredPosition = Vector3.zero;
        private float desiredDistance = 0f;
        private float distanceSmooth = 0f;
        private float preOccludedDistance = 0f;

        void Awake()
        {
            Instance = this;
            _keyboard = Keyboard.current;
            _mouse = Mouse.current;
        }

        void Start()
        {
            Distance = Mathf.Clamp(Distance, DistanceMin, DistanceMax);
            startDistance = Distance;
            Reset();
        }


        void LateUpdate()
        {
            if (TargetLookAt == null)
                return;

            HandlePlayerInput();

            // Check loop
            int count = 0;
            do
            {
                CalculateDesiredPosition();
                count++;
            } while (CheckIfOccluded(count));

            UpdatePosition();
        }

        // OLD INPUT SYSTEM (DO NOT USE - REFERENCE ONLY)
        // public void HandlePlayerInput()
        // {
        //     float deadZone = 0.01f;

        //     if (Input.GetMouseButton(1))
        //     {
        //         // The right mouse button is down - get mouse Axis input
        //         mouseX += Input.GetAxis("Mouse X") * X_MouseSensitivity;
        //         mouseY -= Input.GetAxis("Mouse Y") * Y_MouseSensitivity;

        //         //Debug.Log("Mouse X " + mouseX);
        //         //Debug.Log("Mouse Y " + mouseY);
        //     }

        //     // This is where we will limit mouseY
        //     mouseY = RobotHelper.ClampAngle(mouseY, Y_MinLimit, Y_MaxLimit);

        //     if (Input.GetAxis("Mouse ScrollWheel") < -deadZone ||
        //         Input.GetAxis("Mouse ScrollWheel") > deadZone)
        //     {
        //         desiredDistance = Mathf.Clamp(Distance - Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity,
        //                                       DistanceMin, DistanceMax);

        //         preOccludedDistance = desiredDistance;
        //         distanceSmooth = DistanceSmooth;
        //     }

        // }

        // NEW INPUT SYSTEM (updated 20260617 jc)
        public void HandlePlayerInput()
        {
            float deadZone = 0.01f;

            if (_mouse == null)
                return;

            // if (Input.GetMouseButton(1))
            if (_mouse.rightButton.isPressed)
            {
                Vector2 mouseDelta = _mouse.delta.ReadValue();

                mouseX += mouseDelta.x * X_MouseSensitivity;
                mouseY -= mouseDelta.y * Y_MouseSensitivity;

                // This is where we will limit mouseY
                mouseY = RobotHelper.ClampAngle(mouseY, Y_MinLimit, Y_MaxLimit);
            } 
            else
            {
                // 1. Read the scroll value (returns Vector2, we only need the Y axis for scrolling)
                float scrollY = _mouse.scroll.ReadValue().y;

                // 2. Apply your deadzone check
                if (scrollY < -deadZone ||
                    scrollY > deadZone)
                {
                    // Scrolling has exceeded the deadzone
                    // Scroll values in the new system come in units like 120, so normalize to -1 or 1
                    float scrollDirection = Mathf.Sign(scrollY);

                    // May not need
                    if (scrollDirection > 0)
                    {
                        /* Scroll up */
                    }
                    else
                    {
                        /* Scroll down */
                    }

                    // 3. Update your camera distance calculation
                    // Adjusted: divide by 120 or multiply by a fraction to adapt to the new sensitivity scale
                    desiredDistance = Mathf.Clamp(Distance - (scrollDirection * MouseWheelSensitivity * 0.1f), DistanceMin, DistanceMax);

                    preOccludedDistance = desiredDistance;
                    distanceSmooth = DistanceSmooth;
                }
            }
        }

        void CalculateDesiredPosition()
        {
            // Evaluate distance
            ResetDesiredDistance();
            Distance = Mathf.SmoothDamp(Distance, desiredDistance, ref velocityDistance, distanceSmooth);

            // Calculate desired position (reverse x and y is what we want)
            desiredPosition = CalculatePosition(mouseY, mouseX, Distance);
        }

        Vector3 CalculatePosition(float rotationX, float rotationY, float distance)
        {
            Vector3 direction = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
            return TargetLookAt.position + rotation * direction;
        }

        bool CheckIfOccluded(int count)
        {
            bool isOccluded = false;

            float nearestDistance = CheckCameraPoints(TargetLookAt.position, desiredPosition);

            if (nearestDistance != -1)
            {
                if (count < MaxOcclusionChecks)
                {
                    isOccluded = true;
                    Distance -= OcclusionDistanceStep;

                    // Hardcoded failsafe
                    if (Distance < 0.25f)
                        Distance = 0.25f;
                }
                else
                {
                    // Not Occluded
                    Distance = nearestDistance - Camera.main.nearClipPlane;
                }

                desiredDistance = Distance;
                distanceSmooth = DistanceResumeSmooth;
            }
            return isOccluded;
        }

        float CheckCameraPoints(Vector3 from, Vector3 to)
        {
            float nearestDistance = -1f;

            RaycastHit hitInfo;

            RobotHelper.ClipPlanePoints clipPlanePoints = RobotHelper.ClipPlaneAtNear(to);

            // Draw lines in the editor to make it easier to visualize
            Debug.DrawLine(from, to + transform.forward * -GetComponent<Camera>().nearClipPlane, Color.red);

            Debug.DrawLine(from, clipPlanePoints.UpperLeft);
            Debug.DrawLine(from, clipPlanePoints.LowerLeft);
            Debug.DrawLine(from, clipPlanePoints.UpperRight);
            Debug.DrawLine(from, clipPlanePoints.LowerRight);

            Debug.DrawLine(clipPlanePoints.UpperLeft, clipPlanePoints.UpperRight);
            Debug.DrawLine(clipPlanePoints.UpperRight, clipPlanePoints.LowerRight);
            Debug.DrawLine(clipPlanePoints.LowerRight, clipPlanePoints.LowerLeft);
            Debug.DrawLine(clipPlanePoints.LowerLeft, clipPlanePoints.UpperLeft);


            if (Physics.Linecast(from, clipPlanePoints.UpperLeft, out hitInfo) && hitInfo.collider.tag != "Player")
                nearestDistance = hitInfo.distance;

            if (Physics.Linecast(from, clipPlanePoints.LowerLeft, out hitInfo) && hitInfo.collider.tag != "Player")
                if (hitInfo.distance < nearestDistance || nearestDistance == -1)
                    nearestDistance = hitInfo.distance;

            if (Physics.Linecast(from, clipPlanePoints.UpperRight, out hitInfo) && hitInfo.collider.tag != "Player")
                if (hitInfo.distance < nearestDistance || nearestDistance == -1)
                    nearestDistance = hitInfo.distance;

            if (Physics.Linecast(from, clipPlanePoints.LowerRight, out hitInfo) && hitInfo.collider.tag != "Player")
                if (hitInfo.distance < nearestDistance || nearestDistance == -1)
                    nearestDistance = hitInfo.distance;

            if (Physics.Linecast(from, to + transform.forward * -GetComponent<Camera>().nearClipPlane, out hitInfo) && hitInfo.collider.tag != "Player")
                if (hitInfo.distance < nearestDistance || nearestDistance == -1)
                    nearestDistance = hitInfo.distance;

            return nearestDistance;
        }

        void ResetDesiredDistance()
        {
            if (desiredDistance < preOccludedDistance)
            {
                Vector3 pos = CalculatePosition(mouseY, mouseX, preOccludedDistance);

                float nearestDistance = CheckCameraPoints(TargetLookAt.position, pos);

                if (nearestDistance == -1 || nearestDistance > preOccludedDistance)
                {
                    desiredDistance = preOccludedDistance;
                }
            }
        }

        void UpdatePosition()
        {
            float posX = Mathf.SmoothDamp(position.x, desiredPosition.x, ref velocityX, X_Smooth);
            float posY = Mathf.SmoothDamp(position.y, desiredPosition.y, ref velocityY, Y_Smooth);
            float posZ = Mathf.SmoothDamp(position.z, desiredPosition.z, ref velocityZ, X_Smooth);
            position = new Vector3(posX, posY, posZ);

            transform.position = position;

            transform.LookAt(TargetLookAt);
        }

        void Reset()
        {
            mouseX = 0;
            mouseY = 10;
            Distance = startDistance;
            desiredDistance = Distance;
            preOccludedDistance = Distance;
        }

        public static void UseExistingOrCreateNewMainCamera()
        {
            GameObject tempCamera;
            GameObject targetLookAt;
            RobotCamera myCamera;

            if (Camera.main != null)
            {
                tempCamera = Camera.main.gameObject;
            }
            else
            {
                tempCamera = new GameObject("Main Camera");
                tempCamera.AddComponent<Camera>();
                tempCamera.tag = "MainCamera";
            }

            tempCamera.AddComponent<RobotCamera>();
            myCamera = tempCamera.GetComponent("RobotCamera") as RobotCamera;

            targetLookAt = GameObject.Find("targetLookAt") as GameObject;

            if (targetLookAt == null)
            {
                targetLookAt = new GameObject("targetLookAt");
                targetLookAt.transform.position = Vector3.zero;
            }

            myCamera.TargetLookAt = targetLookAt.transform;

            // FIXME 
            // tempCamera.AddComponent<GraphOverlay>();
        }
    }
};
