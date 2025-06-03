# Good Night v1.0

This repository contains the source code for **Good Night v1.0**, a simple portable Windows Forms tool that can schedule a shutdown or reboot after a specified number of minutes. The interface is in Russian and replicates the layout described in the prompt.

## Building

To build the application you need the .NET SDK (6.0 or later) with Windows desktop components installed. Use the following command from the `GoodNight` folder:

```bash
dotnet build -c Release
```

The resulting executable will be located in `GoodNight/bin/Release/net6.0-windows/`.

## Usage

Run the executable on a Windows machine. Select **Выключить** (shutdown) or **Перезагрузить компьютер** (reboot), enter the number of minutes in the input box and press **Начали**. The app will minimize to the system tray and perform the chosen action after the timer expires. Use **Сброс** to stop the timer or **Выход** to close the application. The tray icon context menu also allows you to show the window or exit the application.

