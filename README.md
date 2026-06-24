# Unity Joystick Robot Simulation

![RoboWorld scene screenshot](Image/RoboWorld.png)

Unity Joystick Robot Simulation is a Unity package combining a joystick GUI and a wheeled robot simulation demo. This repository is intentionally kept simple so readers can quickly review the source code.

## Quick Facts

- Unity version: 6.3 LTS
- Author: Jesse Carpenter
- License: MIT
- Available for purchase on the Unity Asset Store

## What You Get

- Source files for the joystick logic, output display, robot control, and camera behavior
- Input Actions reference file
- Short documentation for usage and release history

## Quick Start

1. Open your Unity 6.3 LTS project.
2. Import the package from the Unity Asset Store.
3. Add the imported objects/scripts to your scene, or open `Source/Scenes/RoboWorld.unity` for reference.
4. Press Play and verify joystick, keyboard, and camera behavior.

## Controls

- Left mouse button: Move joystick grip
- `A` / `D`: Horizontal keyboard input
- `W` / `S`: Vertical keyboard input
- Right mouse button: Rotate main camera around robot
- Mouse wheel: Zoom camera in and out

## Project Notes

- Source is provided for transparency and study.
- The project uses Unity New Input System concepts for joystick and keyboard behavior.
- The joystick output is suitable for differential drive wheeled mobile robot (DWMR) studies.
- Robot motion can be driven from either keyboard input or joystick UI input.
- Two joystick algorithm files are intentionally kept: `Source/JoystickInterface/Scripts/Joystick/Algorithm.cs` and `Source/Robot6/Scripts/Code/Joystick/JoystickAlgorithm.cs`.
- Input System reference: [Unity Input System Workflows](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.19/manual/Workflows.html)

## Source Files

### Joystick GUI

- `Source/JoystickInterface/Scripts/Joystick/Algorithm.cs`
- `Source/JoystickInterface/Scripts/JoystickGraph.cs`
- ~~`Source/ExampleOutputs/Scripts/DisplayOutputs.cs`~~
- `Source/JoystickInterface/ReadMe.txt`

### Robot-6

- `Source/Robot6/Scripts/RobotController.cs`
- `Source/Robot6/Scripts/Code/Joystick/JoystickAlgorithm.cs`
- `Source/Robot6/Scripts/Code/MainCamera/RobotCamera.cs`
- `Source/Robot6/Scripts/Code/Steering/Steer.cs`
- `Source/MeasureMaster/Scripts/Measurements.cs`
- `Source/Scenes/RoboWorld.unity`

## Folder Guide

- `Source/`: Code and related text/json source artifacts
- `Image/`: Screenshot assets used by documentation
- `.gitignore`: Active repository ignore rules
- `ref/Unity.gitignore`: Unity ignore template reference
- `ref/VisualStudioCode.gitignore`: VS Code ignore template reference
- `CHANGELOG.md`: Release history
- `CONTRIBUTING.md`: Contribution guide

## Release Notes

- `Unity Asset Store release`
  - Combined Unity-Joystick-GUI and Robot-6 into a single asset package
  - Asset available for purchase on the Unity Asset Store
  - Added active root `.gitignore` for Unity + VS Code workflows
  - Added repository templates under `ref/`
  - Added `CONTRIBUTING.md` and `CHANGELOG.md`
- `Code review annotations`
  - Added non-functional review comments in source files to highlight runtime setup assumptions and risk points
  - No logic or behavior changes were made

## Updates

- Ongoing maintenance and incremental releases are tracked in this README under **Release Notes**.

## In Progress

- Extend keyboard-to-kinematics behavior so keyboard control mirrors joystick algorithm-style output without directly using the joystick algorithm path.

## License

This project is released under the MIT License. See [LICENSE](LICENSE) for details.

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for contribution workflow and checks.
