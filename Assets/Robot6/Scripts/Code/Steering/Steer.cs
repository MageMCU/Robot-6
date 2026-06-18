
using UnityEngine;
using Joystick;
using System;
using Unity.Mathematics;
using UnityEngine.UIElements;

namespace Steering
{
    public class Steer
    {
        private Transform _botTransform;

        // CONSTANTS
        // MeasureMaster (Free at Unity Store)
        private const float _distanceBetweenWheels = 1.12f;
        private const float _halfDistanceBetweenWheels = 0.56f;
        private const float _inverseHalfDistanceBetweenWheels = 1.78571f;

        // Circumference = 2 * PI * R =  0.9613
        private const float _wheelRadius = 0.153f;

        // VARIABLES
        private float _leftLinearSpeed;
        private float _rightLinearSpeed;
        private Vector3 _CoR;
        private Transform _pivot;

        // ----------------------------------------------------------------------------- Radians per second
        public float LeftAngularSpeed { get { return _leftLinearSpeed / _wheelRadius; } }
        public float RightAngularSpeed { get { return _rightLinearSpeed / _wheelRadius; } }

        //
        public void Initialize(Transform botTransform, Transform pivot)
        {
            _botTransform = botTransform;
            _pivot = pivot;
            _CoR = new Vector3();
        }
        
        // Public Method
        public void Update(int octant, float left, float right)
        {
            // bool turnRight = octant == 8 || octant == 1;
            // bool moveForward = octant == 2 || octant == 3;
            // bool turnLeft = octant == 4 || octant == 5;
            // bool moveBackward = octant == 6 || octant == 7;
            // bool neutral = octant == 0;

            if (_botTransform == null) return;

            // Algorithm Outputs (Linear speeds)
            _leftLinearSpeed = left;
            _rightLinearSpeed = right;

            // These outputs matches the article 1001 Image 5. jc
            // Debug.Log("[" + left + ", " + right + "] <" + octant + ">");


            // Average speed of robot about the pivot point
            float averageSpeed = (_leftLinearSpeed + _rightLinearSpeed) * 0.5f;
            // Debug.Log("[" + _leftLinearSpeed + ", " + _rightLinearSpeed + "] <" + octant + "> averageSpeed: " + averageSpeed);


            // Angular speed of robot about the pivot point
            float angularSpeed = (_leftLinearSpeed - _rightLinearSpeed) / _distanceBetweenWheels;
            // Debug.Log("[" + _leftLinearSpeed + ", " + _rightLinearSpeed + "] <" + octant + "> angularSpeed: " + angularSpeed);


            //
            float radiusOfCoR = averageSpeed / angularSpeed;
            // Debug.Log("[" + _leftLinearSpeed + ", " + _rightLinearSpeed + "] <" + octant + "> radiusOfCoR: " + radiusOfCoR);

            // Joystick Center (0)
            if (radiusOfCoR == math.NAN) return;

            // Forward and Backward
            if (math.abs(radiusOfCoR) == math.INFINITY)
            {
                _botTransform.transform.Translate(Vector3.forward * averageSpeed * Time.deltaTime, Space.Self);
                return;
            }

            // Left and Right turns
            float centerOfRotationDegrees = angularSpeed * Mathf.Rad2Deg;
            if (radiusOfCoR == 0f)
            {
                _botTransform.transform.Rotate(Vector3.up, centerOfRotationDegrees * Time.deltaTime, Space.Self);
                return;
            }

            // ALL OTHER CONDITIONS
            if (octant > 0)
            {
                _CoR.x = _botTransform.transform.position.x + _botTransform.transform.right.normalized.x * radiusOfCoR;
                _CoR.y = _botTransform.transform.position.y + _botTransform.transform.right.normalized.y * radiusOfCoR;
                _CoR.z = _botTransform.transform.position.z + _botTransform.transform.right.normalized.z * radiusOfCoR;

                // Debug.Log("_CoR.x: " + _CoR.x + "_CoR.y: " + _CoR.y + "_CoR.z: " + _CoR.z);

                // transform.RotateAround uses an euler angle
                _botTransform.transform.RotateAround(_CoR, Vector3.up, centerOfRotationDegrees * Time.deltaTime);

                // FIXME
                if (_pivot != null)
                {
                    // DEBUG
                    // Debug.Log("(ALL) CONDITIONS >= 1: " + octant);
                    // Debug.LogFormat("(2) RadiusOfCoR: {0}", radiusOfCoR);
                    // Debug.LogFormat("(3) CoR x{0}, y{1}, z{2}", _CoR.x, _CoR.y, _CoR.z);
                    Debug.DrawLine(_pivot.position, _CoR, Color.green);
                } else Debug.Log("PIVOT NULL");
            }

        }
    }
};
