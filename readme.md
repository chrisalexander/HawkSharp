# HawkSharp

Control the MotorHawk board (http://www.pc-control.co.uk/motor_hawk.htm) through a tasty C# interface.

The standard MotorHawk comes with a DLL and some really nasty managed C++ as a sample application (which I couldn't even get to build).

After a quick p/invoke the DLL is at your whim and I have started on a class wrapper for it.

## Projects

* HawkSharp - class library for wrapping the DLL.
* MotorUI - a nice UI with buttons and keyboard shortcuts for doing standard things with the motors

## Howto

I am unsure of the distribution rights of the hawk.dll file. Hence you will need a copy of it yourself from the CD that comes with MotorHawk.

My default location for it is C:/hawk.dll but you may place it where you like and mofidy the first bit of Hawk.cs to point to it.

## License

This project is BSD licensed. See LICENSE.