# Dali

![Dali](https://raw.githubusercontent.com/FlaviusHouk/Dali/develop/src/Dali/Dali/Resources/Images/Icon.png)

Dali is an application that allows to work with a set of images and able to use them as a reference. It is possible to open image (currently only one) in overlapping semitransparent window that is fully transparent to the user input. That means that all clicks and presses are dispatched to the windows you're currently working on. So, for example, you want to redraw some image in some image processing tool. You can open Dali, load your image and have it semi transparent over any window. And you will be able to work in your image editor seeing image without constant switching between multiple windows. 

Currently applications supports .png, .jpeg, .bmp and .gif images. In future this list will be extended. See our [roadmap](https://github.com/FlaviusHouk/Dali/blob/develop/docs/Roadmap.md) to find information about further development. Also we have a [Architecture overview](https://github.com/FlaviusHouk/Dali/blob/develop/docs/Architecture.md) document to make our architectural vision and tasks easier to understand to people that want to contribute.

## Building
To build application you need to have dotnet SDK 3.1 and higher. Application GUI is built with WPF, so you have to use Windows to built it. 

```
git clone https://github.com/FlaviusHouk/Dali.git
cd src/Dali/RedSharp.Dali
dotnet run
```

## Running 
To run application you need dotnet Runtime 3.1. 
Invoke RedSharp.Dali.exe and enjoy!

### Working with transparent window
Currently only keyboard shortcuts are supported to work with that window. Firstly it appears as semitransparent window that isn't transparent to input. Few commands supported:
* Alt + T: Switches input transparency (makes possible to work with underlying windows);
* Alt + C: Closes transparent window.

