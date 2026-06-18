using UnityEngine;
using UnityEngine.InputSystem;

using Joystick;
using Steering;

public class RobotController : MonoBehaviour
{
    // Singleton
    public static RobotController Instance { get; private set; }

    // Input System (Declaration)
    private Keyboard _keyboard;
    private Mouse _mouse;
    // Gamepad gamepad = Gamepad.current;
    private bool _isKeyboardInputs;

    // private JoystickAlgorithm _joystick;
    private JoystickAlgorithm _algorithm;

    // --- Steer Non-MonoBehaviour Class ---
    private Steer _steer;

    private float _inputX;
    private float _inputY;

    private int _octant;
    private float _left;
    private float _right;

    private float _leftAngularSpeed;
    private float _rightAngularSpeed;
    
    [Header("Joystick Group")]
    [Tooltip("Joystick GUI")]
    [SerializeField] private JoystickGraph _graph;
    [Header("Wheel Meshes")]
    [Tooltip("Wheels")]
    [SerializeField] private Transform leftWheelMesh;
    [SerializeField] private Transform rightWheelMesh;
    [SerializeField] private Transform pivot;

    void Awake()
    {
        // 1. Establish the reference during the first initialization phase
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else 
        {
            Destroy(gameObject);
        }

        // Input System (Instantiation)
        _keyboard = Keyboard.current;
        _mouse = Mouse.current;

        // Instantiate using default Constructor
        // Non-Monobehaviour Class
        _algorithm = new JoystickAlgorithm();
        
        // Instantiate using default Constructor
        _steer = new Steer();
    }

    void Start()
    {
        // Non-Monobehaviour Class
        _algorithm.Initialize();
        // Passing transforms to Non-Monobehaviour Class
        _steer.Initialize(this.transform, pivot);
        // Switch Flag between Keyboard & Mouse
        _isKeyboardInputs = true;
    }

    private void Update()
    {
        ////////////////////////////////////////////////////////////////////////// 
        // Switch between input devices dynamically
        if (Keyboard.current != null &&
        Keyboard.current.anyKey.wasPressedThisFrame)
            _isKeyboardInputs = true;
        else if (Mouse.current != null &&
        Mouse.current.leftButton.wasPressedThisFrame)
            _isKeyboardInputs = false;

        // Execute separate movement methods based on the active device
        if (_isKeyboardInputs)
            HandleKeyboardInput();
        else
            HandleMouseInput();
    }

    private void HandleKeyboardInput()
    {
        // 1. Directly read device states
        // Keyboard keyboard = Keyboard.current;
        if (_graph == null) return;
        
        // New Input System (Quick Test - Not Recommended see TODO)
        // Horizontal
        _inputX = _graph.Horizontal;
        // Vertical
        _inputY = _graph.Vertical;

        // Alternate
        // 1. Directly read device states
        // Keyboard keyboard = Keyboard.current;
        // if (_keyboard == null) return;

        // New Input System (Quick Test - Not Recommended see TODO)
        // Horizontal
        // _inputX = 0f;
        // if (_keyboard.aKey.isPressed && _keyboard.dKey.isPressed) _inputX = 0f;
        // else if (_keyboard.aKey.isPressed) _inputX = -1f;
        // else if (_keyboard.dKey.isPressed) _inputX = 1f;
        // Vertical
        // _inputY = 0f;
        // if (_keyboard.wKey.isPressed && _keyboard.sKey.isPressed) _inputY = 0f;
        // else if (_keyboard.wKey.isPressed) _inputY = 1f;
        // else if (_keyboard.sKey.isPressed) _inputY = -1f;

        // TODO: Gradual input lag from 0 to 1

        //
        RobotMovement();
    }

    

    private void HandleMouseInput()
    {
        // 1. Directly read device states
        // Keyboard keyboard = Keyboard.current;
        if (_graph == null) return;
        
        // New Input System (Quick Test - Not Recommended see TODO)
        // Horizontal
        _inputX = _graph.GripPositionX;
        // Vertical
        _inputY = _graph.GripPositionY;

        // TODO: Gradual input lag from 0 to 1

        //
        RobotMovement();
    }

    // Update is called once per frame
    void RobotMovement()
    {
        // (2) Process Joystick Inputs
        _algorithm.Update(_inputX, _inputY);

        // Joystick Algorithm (updated)
        _octant = _algorithm.Octant;
        _left = _algorithm.Left;
        _right = _algorithm.Right;

        // (3) Steer Robot (updated)
        _steer.Update(_octant, _left, _right);

        // Convert radians to degrees and multiply delta-time
        _leftAngularSpeed = _steer.LeftAngularSpeed * Time.deltaTime * Mathf.Rad2Deg;
        _rightAngularSpeed = _steer.RightAngularSpeed * Time.deltaTime * Mathf.Rad2Deg;

        // (4) Rotate Wheels
        leftWheelMesh.transform.Rotate(Vector3.right, _leftAngularSpeed, Space.Self);
        rightWheelMesh.transform.Rotate(Vector3.right, _rightAngularSpeed, Space.Self);
    }
}
