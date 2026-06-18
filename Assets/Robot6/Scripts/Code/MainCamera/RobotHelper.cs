
using UnityEngine;

namespace MainCamera
{
    public static class RobotHelper
    {
        public struct ClipPlanePoints
        {
            public Vector3 UpperLeft;
            public Vector3 UpperRight;
            public Vector3 LowerLeft;
            public Vector3 LowerRight;
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            do
            {
                if (angle < -360f)
                    angle += 360f;
                if (angle > 360f)
                    angle -= 360f;
            } while (angle < -360f || angle > 360f);

            return Mathf.Clamp(angle, min, max);
        }

        public static ClipPlanePoints ClipPlaneAtNear(Vector3 cameraPosition)
        {
            ClipPlanePoints clipPlanePoints = new ClipPlanePoints();

            if (Camera.main == null)
                return clipPlanePoints;
           
            Transform cameraTransform = Camera.main.transform;

            // Debug.Log("cameraTransform.right" + cameraTransform.right);
            // Debug.Log ("cameraTransform.up" + cameraTransform.up);
            // Debug.Log ("cameraTransform.forward" + cameraTransform.forward);

            float halfFOV = (Camera.main.fieldOfView / 2.0f) * Mathf.Deg2Rad;
            float aspect = Camera.main.aspect;
            float distance = Camera.main.nearClipPlane;
            float height = distance * Mathf.Tan(halfFOV);
            float width = height * aspect;

            clipPlanePoints.LowerRight = cameraPosition + cameraTransform.right * width;
            clipPlanePoints.LowerRight -= cameraTransform.up * height;
            clipPlanePoints.LowerRight += cameraTransform.forward * distance;

            clipPlanePoints.LowerLeft = cameraPosition - cameraTransform.right * width;
            clipPlanePoints.LowerLeft -= cameraTransform.up * height;
            clipPlanePoints.LowerLeft += cameraTransform.forward * distance;

            clipPlanePoints.UpperRight = cameraPosition + cameraTransform.right * width;
            clipPlanePoints.UpperRight += cameraTransform.up * height;
            clipPlanePoints.UpperRight += cameraTransform.forward * distance;

            clipPlanePoints.UpperLeft = cameraPosition - cameraTransform.right * width;
            clipPlanePoints.UpperLeft += cameraTransform.up * height;
            clipPlanePoints.UpperLeft += cameraTransform.forward * distance;

            return clipPlanePoints;
        }

        public static float Map(float X_UnitA, float x1_UnitA, float y1_UnitA, float x2_UnitB, float y2_UnitB)
        {
            // Unit Translation - Use a tolerance for zero
            if (Mathf.Abs(x1_UnitA - x2_UnitB) < 0.001f && Mathf.Abs(y1_UnitA - y2_UnitB) >= 0.001f)
            {
                return X_UnitA * y2_UnitB / y1_UnitA;
            }

            // Unit Translation - Use a tolerance for zero
            if (Mathf.Abs(x1_UnitA - x2_UnitB) >= 0.001f && Mathf.Abs(y1_UnitA - y2_UnitB) < 0.001f)
            {
                return X_UnitA * x2_UnitB / x1_UnitA;
            }
            // Unit Translation - Linear Equation - Point-Slope Form
            // (y - y2) = m(x - x2)
            // slope = (y2 - y1) / (x2 - x1);
            // Return y
            return (y2_UnitB - y1_UnitA) / (x2_UnitB - x1_UnitA) * (X_UnitA - x2_UnitB) + y2_UnitB;
        }
    }
};
