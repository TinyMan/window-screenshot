let edge = require('edge')
let path = require('path')

const screenshot = edge.func(path.join(__dirname, 'screenshot.cs'));

module.exports = screenshot;
