
using UnityEngine;

namespace Joystick
{
    public class Algorithm
    {
        // Local
        private float _outputLeftValue;
        private float _outputRightValue;

        private float _inputX;
        private float _inputY;
        private int _octant;
        
        public void Initialize()
        {
            _inputX = 0f;
            _inputY = 0f;
            _octant = 0;
        }

        // Properties
        public float OutputLeftValue { get { return _outputLeftValue; } }
        public float OutputRightValue { get { return _outputRightValue; } }
        public float OutputOctant { get { return _octant; } }

        // PUBLIC
        public void Update(float inputX, float inputY)
        {
            _inputX = inputX;
            _inputY = inputY;
            _process();
        }

        private void _process()
        {
            const float _tolerance = 0.001f;

            // Output Values
            float leftMotorValue = 0f;
            float rightMotorValue = 0f;

            // Input Difference
            float delta = Mathf.Abs(Mathf.Abs(_inputX) - Mathf.Abs(_inputY));

            // inputX Dominates
            if ((Mathf.Abs(_inputX) < _tolerance) && (Mathf.Abs(_inputY) < _tolerance))
            {
                // Octant Zero: Center (stop)
                leftMotorValue = 0f;
                rightMotorValue = 0f;
                _octant = 0;
            }
            else
            {
                if (Mathf.Abs(_inputX) >= Mathf.Abs(_inputY))
                {
                    if (_inputX >= 0f && _inputY >= 0f)
                    {
                        // Octant 1
                        leftMotorValue = _inputX;
                        rightMotorValue = -delta;
                        _octant = 1;
                    }
                    else if (_inputX <= 0f && _inputY >= 0f)
                    {
                        // Octant 4
                        leftMotorValue = -delta;
                        rightMotorValue = -_inputX;
                        _octant = 4;
                    }
                    else if (_inputX <= 0f && _inputY <= 0f)
                    {
                        // Octant 5
                        leftMotorValue = _inputX;
                        rightMotorValue = delta;
                        _octant = 5;
                    }
                    else if (_inputX >= 0f && _inputY <= 0f)
                    {
                        // Octant 8
                        leftMotorValue = delta;
                        rightMotorValue = -_inputX;
                        _octant = 8;
                    }
                } // inputY dominates
                else if (Mathf.Abs(_inputX) < Mathf.Abs(_inputY))
                {
                    if (_inputX >= 0f && _inputY >= 0f)
                    {
                        // Octant 2
                        leftMotorValue = _inputY;
                        rightMotorValue = delta;
                        _octant = 2;
                    }
                    else if (_inputX <= 0f && _inputY >= 0f)
                    {
                        // Octant 3
                        leftMotorValue = delta;
                        rightMotorValue = _inputY;
                        _octant = 3;
                    }
                    else if (_inputX <= 0f && _inputY <= 0f)
                    {
                        // Octant 6
                        leftMotorValue = _inputY;
                        rightMotorValue = -delta;
                        _octant = 6;
                    }
                    else if (_inputX >= 0f && _inputY <= 0f)
                    {
                        // Octant 7
                        leftMotorValue = -delta;
                        rightMotorValue = _inputY;
                        _octant = 7;
                    }
                }
            }

            _outputLeftValue = leftMotorValue;
            _outputRightValue = rightMotorValue;
        }
    }
};

