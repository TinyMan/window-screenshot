# window-screenshot
Node.Js package using .NET and Edge.js to take screenshot of a given window


# Installation
 
 ```
 npm install --save window-screenshot
 ```
 
# Why this module
 
 I was starting a project and didn't find any module to take a screenshot of a given window only + keep in in memory (no file written to disk).

 
# Example
 
 Take a screenshot of the current foreground window and write to a file:
 ```javascript
let fs = require('fs')
let screenshot = require('../')

screenshot(0, // 0 is for active window
    function (err, res) {
        if(err) throw err;

        fs.writeFile("result.png", res);
    });
 ```
 
# Electron

Check out [Tomas Hubelbauer's fork](https://github.com/TomasHubelbauer/window-screenshot).
 
# Todo
 
 -  Take title/class name of the window and lookup the handle
 
