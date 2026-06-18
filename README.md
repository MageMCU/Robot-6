## Robot-6 
- Unity 3D Universal Project 
### Unity Version
- 6.3 LTS
### Author
- Jesse Carpenter
### Unity Package
- Robot6-003
### Date Released
- 20260617
### License
- MIT
### History
- Robot 6 was an **old project (2018)** revised from Unity versions 2018 - 2019. Much of the code was rewritten for the New Input System including the basic kinematics to drive the orientation and wheels of the robot.
### Bug Fix & notes
- Robot 6 Input System
  - Using Joystick Interface (Joystick Group Prefab)
    - Keyboard
    - Mouse
- Unity 6.3 LTS Rigidbody
  - Mass: 3.8 kg (about the mass of a gallon of water)
  - Linear Damping: 0.1
  - Angular Damping: 0.05
  - Automatic Center of Mass: Checked
  - Automatic Tensor: Checked
  - Use Gravity: Checked
  - Is Kenematic: Un-Checked (Do Not Use)
  - Interpolate: None
  - Collision Detection: Discrete
  - Remaining Settings: Default
- There are two identical joystick algorithms
  - Joystick Interface: Algorithm.cs
  - Robot 6 code: JoystickAlgorithm.cs
- Measure Master (Unity Store)
  - Robot 6
    - Left & Right Wheel Meshes
      - Tire: attached Measurements (script)
        - Tire Radius: used for kinematics
        - Distance between tires: used for robot pose (orientation)
  - Robot Camera
    - Presently using Old Input System - FIXME [jc 20260617 ok]
      - Project Settings
        - Player: Active Input Settings: DO NOT USE - Both
  - INPUTS
    - (1) Keyboard: WASD keys: Moves robot.
    - (2) Left Mouse Button: Joystick GUI: Moves joystick grip (Joystick Camera as Overlay see Main Camera)
    - (3) Right Mouse Button: Main Camera Control on robot: Rotate around robot (See Robot Camera FIXME)
    - (4) Scroll Wheel:  Main Camera Control on robot: Zoom in and out from robot.
### TODO
- A lot.... The Robot 6 will be posted at MageMCU to be retired for Robot 7.
