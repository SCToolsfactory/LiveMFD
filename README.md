# LiveMFD V 0.9.0.10    

Provides a framework to animate and customize TM Cougar MFD Panels. 

### General note to builders
The Project files expect referenced Libraries which have no NuGet package reference in a Solution directory  ´ExtLibraries´  
Those external libraries can built from the project SC_Toolbox:  
https://github.com/SCToolsfactory/SC_Toolbox

Projects included:

* MFDLib - the framework library to build MFD underlays from
* MFDflight - an example MFD underlay SC Flight configuration
* MFDstartLand - an example MFD underlay SC Start and Land configuration
* TEST_MFD - an example WinForm project that makes use of the above

In general config files need to be adopted to the use case and the desired commands, either joystick or keyboard commands or macros are available.  

Each MFD underlay has a 'behind code' BC class which takes care of some business duty.

The example includes a nice font for instruments 'Share Tech Mono' see Credits..

-----

### Config Files:

The command is Json syntax, supports also keystrokes sent to the active window.

1st level items:

_{  
    "_Comment" : "Cougar MFD Panel Test Config File 1",  
    "BackgroundImage":  "someImage.png",  
    "MapName"" : "TestConfig1",  
    "Macros" : [ .. ],  
    "SwitchMap": [ .. ],  
    "ButtonLabels": [ .. ]  
}_  


where Macros:

_{   
    "MName": "_FullPwr", "CmdList": [ { "S": {"Index": 1, "Value": 1000 }}, { "S": {"Index": 2, "Value": 1000 }} ]   
},_  


where SwitchMap: a list of 28 Input items

_{  
    "Input": "BT6",  "Cmd" : [{ "K": {"VKcodeEx": "VK_B", "Mode": "t", "Modifier": ""}}]  
},_

1st Cmd item is for Press, an optional second for release of a pushbutton (above only a press event is triggered)


where ButtonLabels: a list of 28 strings to be shown in the MFD underlay (use ¬ to subst. a crlf)

See also examples included

A full list of the syntax can be found here:
https://github.com/SCToolsfactory/SC_Toolbox/tree/master/vjMappingLibrary/Doc

Note:  
You have to install the vJoy (V 2.1) driver.  
The driver is not available here but use this link.  
http://vjoystick.sourceforge.net/site/index.php/download-a-install/download     

Commands are submitted to a SCJoyServer as UDP messages.  
https://github.com/SCToolsfactory/SCJoyServer

You have to change the default IP/Port for your setup.


# Usage 

In order to use this framework one has to add the library DLL 

Just within the application Exe folder:  
* vjMapper.dll                 Command Mapping Library

Expects Config files in a 'ConfigFiles' folder below the Exe File location

Those external libraries can built from the project SC_Toolbox:  
https://github.com/SCToolsfactory/SC_Toolbox


## Credits

Share Fonts included:

Copyright (c) 2012, Carrois Type Design, Ralph du Carrois (www.carrois.com), with Reserved Font Name 'Share'

This Font Software is licensed under the SIL Open Font License, Version 1.1. and is available with a FAQ at:
http://scripts.sil.org/OFL  

Use References to:
* DirectX11-net40\SharpDX.dll
* DirectX11-net40\SharpDX.DirectInput.dll

