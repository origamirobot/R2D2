# R2D2
R2D2 controller system written in C# for Raspberry Pi.




## Setup

Enable I2C interface on Raspberry Pi

> sudo raspi-config
> Interface Options
> Enable/Disable automatic loading of I2C kernel module

Enable SSH Deploy In .NET

> dotnet tool install -g dotnet-sshdeploy


Detect I2C devices connected to Raspi

> sudo i2cdetect -y 1


### Controller Setup

Update package manager and install XBox Controller drivers.

> sudo apt update
> sudo apt upgrade
> sudo apt install xboxdrv
> sudo apt install joystick
> sudo jstest /dev/input/js0



## Serial Communications

Raspberry Pi uses pins 8 (TX) and 10 (RX) for serial communications. On Raspberry Pi 3B, there are two serial ports: UART0 and UART1. If Bluetooth is enabled, it will take over UART0 leaving only UART1

Disable Bluetooth
> sudo nano /boot/config.txt
> dtoverlay=disable-bt


Detect if a serial device is connected
> dmesg | grep "ttyA"



