# Contributing

Thanks for helping improve Robot-6.

## Scope

This repository accepts improvements to:
- Robot control behavior and kinematics scripts
- Joystick and camera interaction behavior
- Documentation, package notes, and release history

## Development Setup

1. Use Unity 6.3 LTS.
2. Open this repository as a Unity project, or import `Robot6-003.unitypackage` for package-level testing.
3. Validate updates in `Source/Scenes/RoboWorld.unity`.

## Coding Guidelines

- Keep script changes focused and small.
- Keep comments concise and technical.
- Keep public behavior compatible unless a release note documents a breaking change.

## Pull Request Checklist

- Verify robot movement from keyboard input works as expected.
- Verify joystick drag and camera controls work in Play mode.
- Update `README.md` when behavior, controls, or folder structure changes.
- Add an entry in `CHANGELOG.md`.

## Versioning and Releases

- Package releases use the `Robot6-xxx.unitypackage` naming pattern.
- Record release notes in `CHANGELOG.md` and summarize latest release in `README.md`.

## License

By contributing, you agree that your contributions are licensed under the MIT License used by this repository.
