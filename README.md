# Shutdown Timer

This repository contains a small Python script `shutdown_timer.py` that shuts down the computer after a specified delay.

## Usage

```
python3 shutdown_timer.py <seconds>
```

Replace `<seconds>` with the number of seconds to wait before initiating the shutdown.

The script detects the operating system and runs the appropriate shutdown command for Windows, Linux, or macOS. If the operating system is not supported or the shutdown command fails, an error message will be printed.
