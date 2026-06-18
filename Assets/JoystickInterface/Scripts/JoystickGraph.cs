// Author: Jesse Carpenter
// Project: Unity Joystick GUI 
// Unity: Universal 3D
// Website: carpentersoftware.com
// Email: hello@carpentersoftware.com
// Github: MageMCU 
// Repository: Unity-Joystick-GUI
// Created: Long Long Ago 
// Rework Unity 6.3 LTS: 20260527
// Revision: 20260607
// Update: Unity 6 to New Input System:
// * Input: Left Mouse Button
// * Output: -1 to 1 where center is 0.0
// Debug Fix: Voltage Simulation
// Note that a Dead-Zone around grip center
//      might be calculated in parent class.
// License: MIT
//

using UnityEngine;
using UnityEngine.InputSystem;
using Joystick;


public class JoystickGraph : MonoBehaviour
{
    // Input System
    Keyboard _keyboard;
    Mouse _mouseInput;
    // Gamepad gamepad = Gamepad.current;

    // Joystick Algorithm
    private Algorithm _algorithm;

    // Mouse Screen Positions
    private Vector3 _screenPixelPosition;
    private Vector3 _worldPosition;
    private Vector3 _mousePosition;

    // Map Texture used for plane
    private Vector3 _JoystickMapVector;
    // position from 0 to 1 (normalized)

    // Vector 3D Grip Position
    private Vector3 _gripPosition;

    // Joystick can be moved anyworld in the Unity Scene
    // because of the World Offset.
    private Vector3 _worldOffset;

    // Reference Point
    private Vector3 _targetZero;

    // Boolean variable
    private bool _mouseDrag;

    // Mouse Speed LERP like animation
    private float _mouseSpeed;

    // The boundary (5) used for map texture
    // see _JoystickMapVector (aligned to  5 units)
    private float _joystickBoundary;

    [Header("Joystick Voltage Simulation")]
    [Tooltip("Potentiometer X")]
    [SerializeField] private float _potX;
    [Tooltip("Potentiometer Y")]
    [SerializeField] private float _potY;
    [SerializeField] private bool _studyVoltages;
    // Getters
    public float VoltageX { get { return _potX; } }
    public float VoltageY { get { return _potY; } }
    public bool StudyVoltages { get { return _studyVoltages; } }

    [Header("Joystick Grip Positions")]
    [Tooltip("Postion X")]
    [SerializeField] private float _gripPositionX;
    [Tooltip("Postion Y")]
    [SerializeField] private float _gripPositionY;
    [Tooltip("Magnitude (X, Y)")]
    [SerializeField] private float _gripMagnitude;
    [SerializeField] private bool _studyPositions;
    public float GripPositionX { get { return _gripPositionX; } }
    public float GripPositionY { get { return _gripPositionY; } }
    public float GripUnitMagnitude { get { return _gripMagnitude; } }
    public bool StudyPositions { get { return _studyPositions; } }

    [Header("Joystick Sub-Unit Vector")]
    [Tooltip("Vector X")]
    [SerializeField] private float _joystickVectorX;
    [Tooltip("Vector Y")]
    [SerializeField] private float _joystickVectorY;
    [Tooltip("Magnitude (X, Y)")]
    [SerializeField] private float _joystickMagnitude;
    [SerializeField] private bool _studySubUnitVector;
    public float GripSubUnitX { get { return _joystickVectorX; } }
    public float GripSubUnitY { get { return _joystickVectorY; } }
    public float SubUnitMagnitude { get { return _joystickMagnitude; } }
    public bool StudySubUnitVector { get { return _studySubUnitVector; } }


    [Header("Joystick Inputs to Algorithm Outputs")]
    [Tooltip("Left Wheel")]
    [SerializeField] private float _left;
    [Tooltip("Right Wheel")]
    [SerializeField] private float _right;
    [Tooltip("Debug Octant for proper wheel rotation")]
    [SerializeField] private float _octant;
    [SerializeField] private bool _studyAlgorithmOutputs;
    public float JoystickLeft { get { return _left; } }
    public float JoystickRight { get { return _right; } }
    public float Octant { get { return _octant; } }
    public bool StudyAlgorithmOutputs { get { return _studyAlgorithmOutputs; } }

    [Header("Keyboard Inputs")]
    [Tooltip("AD keys for Horizontal movement")]
    [SerializeField] private float _keyboardHorizontal = 0f;
    [Tooltip("WS keys for Vetical movement")]
    [SerializeField] private float _keyboardVertical = 0f;
    [Tooltip("Left")]
    [SerializeField] private float _normalizedLeftSpeed;
    [Tooltip("Right")]
    [SerializeField] private float _normalizedRightSpeed;
    public float Horizontal { get { return _keyboardHorizontal; } }
    public float Vertical { get { return _keyboardVertical; } }
    [SerializeField] private bool _studyKeyboardInputs;
    public float NormalizeLeftSpeed { get { return _normalizedLeftSpeed; } }
    public float NormalizeRightSpeed { get { return _normalizedRightSpeed; } }
    public bool StudyKeyboardInputs { get { return _studyKeyboardInputs; } }

    [Header("Robot Configuration")]
    [Tooltip("Distance between the two driving wheels in meters")]
    [SerializeField] private float trackWidth = 1.12f; // meters - used MeasureMaster
    [Tooltip("Radius of the drive wheels in meters")]
    // [SerializeField] private float wheelRadius = 0.153f; // meters - used MeasureMaster
    // [Tooltip("Max linear speed limit of the robot in m/s")]


    [Header("LERP for Keyboard Inputs")]
    [SerializeField] private float _acceleration; // Speed at which the value goes from 0 to 1
    [SerializeField] private float _deceleration; // Speed at which the value drops back to 0


    [Header("Drag GameObjects Here")]
    [SerializeField] private GameObject _grip;
    [SerializeField] private GameObject _handle;
    [SerializeField] private GameObject _anchor;
    [SerializeField] private Camera _overlayCamera;

    private void Start()
    {
        // Assignments
        _keyboard = Keyboard.current;
        _mouseInput = Mouse.current;
        _mouseDrag = false;
        _mouseSpeed = 15f;
        _joystickBoundary = 5;
        _acceleration = 5f;
        _deceleration = 5f;
        // Joystick Algorithm
        _algorithm = new Algorithm();
        // JosystickGroup Transform Position
        _worldOffset = this.transform.position;

    }

    private void Update()
    {
        ////////////////////////////////////////////////////////////////////////// 
        // Switch between input devices dynamically
        if (Keyboard.current != null &&
        Keyboard.current.anyKey.wasPressedThisFrame)
            _studyKeyboardInputs = true;
        else if (Mouse.current != null &&
        Mouse.current.leftButton.wasPressedThisFrame)
            _studyKeyboardInputs = false;

        // Execute separate movement methods based on the active device
        if (_studyKeyboardInputs)
            HandleKeyboardInput();
        else
            HandleMouseInput();
    }

    private void HandleKeyboardInput()
    {
        // 1. Directly read device states
        // Keyboard keyboard = Keyboard.current;
        if (_keyboard == null) return;

        // 2. Determine target values (0 or 1) based on raw binary state
        float targetVertical = 0f;
        if (_keyboard.wKey.isPressed) targetVertical += 1f;
        if (_keyboard.sKey.isPressed) targetVertical -= 1f;

        float targetHorizontal = 0f;
        if (_keyboard.dKey.isPressed) targetHorizontal += 1f;
        if (_keyboard.aKey.isPressed) targetHorizontal -= 1f;

        // 3. Gradually lerp/move towards the target (creates the smooth "lag")
        float currentAccel = (Mathf.Abs(targetVertical) > 0.01f) ? _acceleration : _deceleration;
        _keyboardVertical = Mathf.MoveTowards(_keyboardVertical, targetVertical, currentAccel * Time.deltaTime);

        float currentHorizAccel = (Mathf.Abs(targetHorizontal) > 0.01f) ? _acceleration : _deceleration;
        _keyboardHorizontal = Mathf.MoveTowards(_keyboardHorizontal, targetHorizontal, currentHorizAccel * Time.deltaTime);
    }

    private void HandleMouseInput()
    {
        _mouseDrag = false;
        _octant = _algorithm.OutputOctant;

        // Input Mouse On Drag
        if (_mouseInput != null)
        {
            // Check if the left mouse button is pressed
            if (_mouseInput.leftButton.isPressed)
            {
                // Get the current mouse screen position
                Vector2 mousePos = _mouseInput.position.ReadValue();
                Ray ray = _overlayCamera.ScreenPointToRay(mousePos);

                // Cast a ray into the scene to detect if it hit a collider
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (_mouseInput.leftButton.isPressed) MouseDrag();
                }
            }
        }

        // Joystick Grip Animation
        if (_grip != null)
        {
            // Animate Joystick Grip Handle (Speed cause a lag which is favorable))
            if (_mouseDrag)
            {
                // Player is chasing the MOUSE (mousePosition + worldOffsetTransform.position)
                _grip.transform.position = Vector3.MoveTowards(_grip.transform.position,
                    _mousePosition + _worldOffset, _mouseSpeed * Time.deltaTime);
            }
            else
            {
                // Player is chasing the TARGET (targetZero + worldOffsetTransform.position)
                _grip.transform.position = Vector3.MoveTowards(_grip.transform.position,
                    _targetZero + _worldOffset, _mouseSpeed * Time.deltaTime);
            }

            // Grip Handle Animation follows Joystick Grip Animation
            if (_handle != null && _anchor != null)
            {
                // Coordinate system independent
                _handle.transform.LookAt(_grip.transform.position);

                // Coordinate system independent
                Vector3 MidPosition = (_grip.transform.position - _anchor.transform.position) * 0.5f;

                // handle position dependent on world offset
                _handle.transform.position = MidPosition + _worldOffset;

                // Literal should match the scale-z on Handle Transform
                float scaleZ = (_grip.transform.position - _anchor.transform.position).magnitude;
                if (scaleZ <= 0.3f)
                    scaleZ = 0.3f;

                // Coordinate system independent
                _handle.transform.localScale = new(0.3f, 0.3f, scaleZ);
            }

            // SET VECTORS
            // 2.5 is the grip center
            // Joystick Map is 10m squared
            _JoystickMapVector = _worldOffset - _grip.transform.position;
            // 0.0 is the grip center
            // Joystick Range from -1 to 1 on both X and Y axis.
            // The Joystick Map is 10m squared so divide by 5 (* 0.2)
            _gripPosition = _JoystickMapVector * 0.2f;


            // Joystick Simulations (Grip Positions)
            JosystickSimulations();
        }
    }

    private void JosystickSimulations()
    {
        // (1) Grip Positions
        if (_studyPositions) GripPositions();

        // (2) Convert Grip Positions to Vector
        if (_studySubUnitVector) GripVector();

        // (3) Output Voltage 0 to 5 (example only)
        if (_studyVoltages) PotentiometerSimulation();

        // (4) Output Joystick Algoritm (pertinent)
        // Includes both squared bounding area and
        // unit vector.
        if (_studyAlgorithmOutputs) JoystickOutput();

        // (5) Keyboard elsewhere
    }

    private void MouseDrag()
    {
        // Is there a mouse device attached
        if (_mouseInput != null)
        {
            _mouseDrag = true;

            // Mouse to Screen Pixel Position
            _screenPixelPosition = _mouseInput.position.ReadValue();

            // Pixel Position to world position
            _worldPosition = _overlayCamera.ScreenToWorldPoint(_screenPixelPosition);

            // World to Local coordinates
            float localX = _worldPosition.x - _worldOffset.x;
            float loaclZ = _worldPosition.z - _worldOffset.z;

            // Bound Current Position X to Boundary
            if (localX < -_joystickBoundary) localX = -_joystickBoundary;
            else if (localX > _joystickBoundary) localX = _joystickBoundary;
            // Limit Current Position Z to Boundary
            if (loaclZ < -_joystickBoundary) loaclZ = -_joystickBoundary;
            else if (loaclZ > _joystickBoundary) loaclZ = _joystickBoundary;

            // Mouse Position (local coordinates)
            _mousePosition = new Vector3(localX, 0.0f, loaclZ);
        }
    }

    private void GripPositions()
    {      // To debug, view values in the Inspector
        if (_grip != null)
        {
            // Joystick GUI
            _gripPositionX = _gripPosition.x;
            _gripPositionY = _gripPosition.z; // Grip Vector

            Vector2 vector = new Vector2(_gripPosition.x, _gripPosition.z);
            // Grip Megnitude
            _gripMagnitude = vector.magnitude;
        }
    }

    private void GripVector()
    {
        // To debug, view values in the Inspector
        if (_grip != null)
        {
            // Grip Vector
            Vector2 vector = new Vector2(_gripPosition.x, _gripPosition.z);
            // Sub-Unit Magnitude
            _joystickMagnitude = vector.magnitude;

            // Check bounds
            if (_joystickMagnitude > 1f) _joystickMagnitude = 1f;
            // Normalize the vector and multiply by
            // the scalar (mag). Although the mag of a
            // unit vector is always one, the mag might
            // be less than 1 correlated with the values
            // that match the joystick input.
            vector = vector.normalized * _joystickMagnitude;


            // Assign its value for debug
            _joystickVectorX = vector.x;
            _joystickVectorY = vector.y;
        }
    }

    private void PotentiometerSimulation()
    {
        // Simulation INPUT of two potentiometers on
        // the x-coordinate and on the y-coordinate.
        // To debug, view values in the Inspector
        float maxVoltage = 5f;

        // The joystick bounding area (Output -5 to 5)
        if (_grip != null)
        {
            // Input Grip Position range form -1 to 1
            // a =_gripPositionX + 1 = 2
            // b= a / 2
            // c= b * maxVoltage
             // Output x & y range from 0 to 5
            float x = (_gripPositionX + 1) * 0.5f * maxVoltage;
            float y = (_gripPositionY + 1) * 0.5f * maxVoltage;

            // 2.5 is the grip center 
            // X
            _potX = x;
            if (_potX > maxVoltage) _potX = maxVoltage;
            if (_potX < 0.0f) _potX = 0.0f;
            // Y
            _potY = y;
            if (_potY > maxVoltage) _potY = maxVoltage;
            if (_potY < 0.0f) _potY = 0.0f;
        }
    }

    private void JoystickOutput()
    {
        // Uses joystick algorithm
        // Its output is not a vector...
        // To debug, view values in the Inspector
        if (_grip != null)
        {
            // Joystick Algorithm (Sub-Unit Vector dips down at points)
            // A real joystick uses voltages 
            // (potentiometer X) and 
            // (potentiometer Y) that could be mapped to (Grip Positions)
            _algorithm.Update(_gripPositionX, _gripPositionY);
            // This is not a vector but torque simulation
            // for each of the differential drive wheeled
            // mobile robot. 
            _left = _algorithm.OutputLeftValue;
            _right = _algorithm.OutputRightValue;
        }
    }
}
