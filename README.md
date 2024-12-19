# NITHemulation
_An extension for the NITH framework to provide some emulation capabilities._

<div align="center">
  <img src="NithLogo_White_Trace.png" alt="NITH logo." width="150px"/>
</div>

It can be used to easily add Mouse and Keyboard emulation capabilities to your accessible application.

## Features
It provides the following functionalities:

- **Keyboard Emulation**: 
  - Utilizes the [RawInputProcessor](https://www.nuget.org/packages/RawInputProcessor) NuGet package for handling raw keyboard input.
  - Supports key up and key down events, as well as multiple keyboard handling.
  - Primary classes include:
    - `KeyboardModuleWPF`: For WPF applications.
    - `KeyboardModuleForms`: For Windows Forms applications.
  - Additional enums:
    - `LKeyPressStates`: Lists logical key press states (e.g., key down/up).
    - `LVKeyNames`: Contains all virtual key codes.
    - `VKeyCodes`: Lists all keys that can be pressed.

- **Mouse Emulation**:
  - Comprises a `MouseSender` and a `MouseReceiverModule`.
  - `MouseSender`: Static class for sending mouse events (e.g., clicking, moving the cursor).
  - `MouseReceiverModule`: Stores `IMouseBehavior` objects to receive and process mouse input.
    - Supports adjustable polling rates and two modes: Normal and FPS.
    - Normal mode returns a `MouseModuleSample` with velocity, position, and direction.
    - FPS mode emulates behavior in first-person shooter games, focusing on velocity and direction while keeping the cursor stationary.
    - Includes `NithSensorBehavior_GazeToMouse` to convert gaze coordinates into mouse movements.

- **Console Functionality**:
  - The `Console` namespace includes the `ConsoleTextToTextBox` class, which redirects console output to a WPF `TextBlock` control, useful for displaying sensor debug data and other information in the application interface.
