# fanspeed
README Oct 2020

A simple plugin dll for LCD Smartie taht allows smartie to read temperatures and fan speeds from the "Open Hardware Monitor" published WMI Sensor data.

### Usage in LCD Smartie
In the Screens Settings Menu:

CPU T/F: $dll(fanspeed,1,CPU Package,0)/$dll(fanspeed,2,CPU,0)
MOB T/F: $dll(fanspeed,1,Chassi,0)/$dll(fanspeed,2,Chassi,0)

