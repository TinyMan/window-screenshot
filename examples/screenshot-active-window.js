let fs = require('fs')
let screenshot = require('../')

screenshot(0, // 0 is for active window
    function (err, res) {
        if(err) throw err;

        fs.writeFile("result.png", res);
    });
