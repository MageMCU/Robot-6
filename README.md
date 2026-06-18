# Robot-6

![RoboWorld scene screenshot](Image/RoboWorld.png)

Robot-6 is a Unity robot demo that lets you drive a wheeled robot with either the keyboard or an on-screen joystick. The repo includes the full Unity project and a `.unitypackage` version for distribution.

## Quick Facts

- Unity version: 6.3 LTS
- Author: Jesse Carpenter
- License: MIT
- Package name: `Robot6-003`

## What You Get

- A robot controller with wheel movement and steering logic
- An on-screen joystick interface
- A main robot camera with mouse look and zoom
- Measurement helpers used for wheel and robot sizing
- Example scenes for testing and demonstration

## How To Open It

1. Open the project in Unity 6.3 LTS.
2. Open the scene at `Assets/Scenes/RoboWorld.unity`.
3. Press Play.

If you only want to import the packaged version, use `Robot6-003.unitypackage`.

## Controls

- `WASD`: Move the robot
- Left mouse button: Move the joystick grip
- Right mouse button: Rotate the main camera around the robot
- Mouse wheel: Zoom the main camera in and out

## Project Notes

- The project uses the Unity Input System.
- The joystick UI and the robot controller both feed the same movement logic.
- There are two joystick algorithm files: `Algorithm.cs` belongs to the self-contained JoystickInterface package, and `JoystickAlgorithm.cs` belongs to the Robot6 controller. Both are kept so each package can stand alone.
- The repository includes reference copies of Unity and VS Code ignore files under `ref/`.

## Folder Guide

- `Assets/Robot6`: Robot scripts, prefabs, meshes, and materials
- `Assets/JoystickInterface`: Joystick UI assets and scripts
- `Assets/MeasureMaster`: Measurement tools used by the robot project
- `Assets/Scenes`: Main scenes
- `ref/`: Reference ignore templates

## License

This project is released under the MIT License. See [LICENSE](LICENSE) for details.
