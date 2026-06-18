
using UnityEngine;
using Algebra;

namespace Compass
{
    public class Compass
    {
        // Private Objects
        private Timer.Timer _timer;
        private Vector _vector;
        private float _compassAxisY;
        private float _compassReadRate;

        // Private Variables
        private float _xHeading;
        private float _yHeading;

        // Constructor
        public Compass()
        {
            // Instantiate Timer Object
            _timer = new Timer.Timer();

            // Instaniate vector
            _vector = new Vector(2);

            // Set Default Read Rate 3.25 Hz (cycles per second)
            CompassReadRate = 3.25f;

            // Set Default Timer Interval
            // to seconds for each compass reading.
            _timer.TimeInterval = 1f / _compassReadRate;
        }

        // PROPERTIES
        public float CompassAxisY { get { return _compassAxisY; } }
        public float CompassReadRate { set { _compassReadRate = value; } }

        // PUBLIC Methods
        public Vector ReadCompass(float rotationalAxis = 0f)
        {
            // Read the Y-Axis of the Robot Heading
            _compassAxisY = rotationalAxis;

            // Read Compass
            _readCompass();

            // Assign Vector Components for Robot Heading
            _vector.X = _xHeading;
            _vector.Y = _yHeading;

            return _vector;
        }

        // PRIVATE
        // Main Method for a GameObject Controller Update()...
        private void _readCompass()
        {
            // Timer Condition
            if (_timer.IsTimer)
            {
                // Compass Reading at compass read rate (M-ODR)
                float rad = _compassAxisY * /* Constant.AngleToPI; */ Mathf.Deg2Rad;
                // ASSIGN - current heading with input angle in radians.
                // THIS SHOULD BE THE ONLY FOR ASSIGNMENT
                // WAS: _heading = new Vector(Mathf.Cos(rad), Mathf.Sin(rad), 0f);
                _xHeading = Mathf.Cos(rad);
                _yHeading = Mathf.Sin(rad);
            }
        }

        // TODO - introduce background noise
    }
};