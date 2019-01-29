# WindowPositionSaver
WPF library for saving and restoring window position on the screen. Works independently for 3 different monitor configurations.
### Add WindowPositionSaver DLL to references and usings and then place this 3 methods to your window code:
Constructor:
```sh
WPS.WPS_Window_Constructor(this);
```
Window_Loaded:
```sh
WPS.WPS_Window_Loaded(this);
```
Window_Closing:
```sh
WPS.WPS_Window_Closing(this);
```
### And it will work! 