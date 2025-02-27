# NITHemulation
_An extension for NITHlibrary to provide some emulation capabilities._

<div align="center">
  <img src="NithLogo_White_Trace.png" alt="NITH logo." width="150px"/>
</div>
<br>

## Overview

NITHemulation is a [NITHlibrary](https://github.com/LIMUNIMI/NITHlibrary) extension which can be used to easily add Mouse and Keyboard emulation capabilities to your accessible application.

_At its current state, NITHemulation is compatible only with **Windows** operating systems, due to calls to the system's core libraries for input/output management. Moreover, it's compatible and depends on **Windows Presentation Foundation (WPF)** libraries._

## Usage and dependencies
As like as NITHlibrary, NITHemulation is built using C# within __Microsoft Visual Studio__. We recommend using the same for an optimal development experience.
In order to use NITHemulation you can simply clone this repo, place its folder next to your project folder and add a reference to it within your project (and/or add it to your __Solution__ in __Visual Studio__). You will also need to clone or download [NITHlibrary](https://github.com/LIMUNIMI/NITHlibrary) from which NITHemulation depends on, and place it in an adjacent folder.

- [ ] Precompiled DLLs will be available in the future.

## Contents overview
It provides the following functionalities, through the respective packages.

### Keyboard input
The `Keyboard` namespace provides a structured and in-depth way for handling keyboard __input__ events within applications. It facilitates the capture and processing of keyboard events, allowing for customizable behaviors in response to user actions.
Being based on the [RawInputProcessor](https://www.nuget.org/packages/RawInputProcessor) NuGet package, it can notably support multiple keyboard handling.

- **KeyboardModuleWPF**: This module integrates keyboard input handling into WPF applications. It sets up raw input processing, allowing the application to capture keyboard events directly from the operating system. The module manages a list of behaviors that can be executed in response to key events, enabling dynamic and flexible input handling.

- **IKeyboardBehavior**: defines an interface for keyboard behaviors. A behavior receive and handles a `RawInputEventArgs` object.

- **Key Press States and Virtual Key Names**: The namespace includes constants defining the states of key presses, such as when a key is pressed down or released. Additionally, it contains a comprehensive enumeration of virtual key names, representing various keyboard keys, including letters, numbers, function keys, and special characters.

_NOTICE: We didn't introduce extra tools to emulate keyboard **output**, since there are already well refined libraries such as [InputSimulator](https://github.com/michaelnoonan/inputsimulator) or native methods that can carry out the job fine._

### Mouse input/output
The `Mouse` namespace in the NITHlibrary provides a comprehensive framework for handling mouse input and output, enabling applications to interact with the mouse in a structured manner. It includes interfaces and classes that facilitate the capture of mouse movements, button presses, and other related events.

- **Mouse Input**: the core of mouse _input_ sampling system is encapsulated in the `MouseReceiverModule`, which samples mouse position and velocity at specified polling rates. This module allows for different polling modes, such as normal mode, where the mouse is free to move, and FPS mode, where the mouse is locked to the center of the screen (simulating the behavior typical of First Person Shooter videogames, which is particularly useful in certain scenarios). The module also manages a list of behaviors that can be applied to mouse samples, enabling custom responses to mouse actions.

- **IMouseBehavior**: this interface can be extended to define behaviors that react to mouse input. These behaviors can then be added to the `MouseReceiverModule`.

- **Mouse Output**: the `MouseSender` static class provides methods to send mouse events and simulate mouse functions. It allows for simulating mouse button actions, wheel movements, and setting the cursor's position programmatically. Additionally, it enables control over the visibility of the mouse cursor. It leverages _PInvoke_ to access Windows API functions for manipulating the mouse and retrieving its position.

There's a little extra, represented by the following:

- **NithSensorBehavior_GazeToMouse**: this specifically focuses on translating gaze coordinates from gaze tracking sensors into mouse cursor positions. When added to a `NithModule` connected to, this behavior listens for incoming sensor data containing gaze information and updates the mouse cursor's position accordingly. We introduced this facility since the typical application for gaze detection is to control on-screen selection operations: this can be combined with cursor hiding.

### Extra: Console emulation
The `ConsoleTextToTextBox` class in the `ConsoleEmulation` namespace is a custom `TextWriter` designed to redirect console output to a `TextBlock` control in a WPF application. This functionality is particularly useful because WPF does not support console output natively, and `NithModule` and other components of `NITHlibrary` are designed to provide some informative console output upon some events (e.g. sensor connection). It manages a queue of strings to maintain a maximum number of lines displayed, ensuring that the oldest lines are removed as new output is added. When writing to the `TextBlock`, it updates the text and ensures that the `ScrollViewer` scrolls to the latest output.

## License
This project is licensed under the [GNU GPLv3 license](https://www.gnu.org/licenses/gpl-3.0.en.html).
