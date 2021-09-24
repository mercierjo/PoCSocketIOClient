'use strict';

const http = require('http');
const socket = require('socket.io');
const server = http.createServer();
const port = 11004;
const prefix = "server: ";

var io = socket(server, {
    //options
});

io.on('connection', socket => {
    socket.on('echo', cb => {
        cb(new Date().getTime())
    });
});

//setInterval(() => {
//    io.emit('heartbeat', new Date().getTime())
//}, 5000)

server.listen(port, () => {
    console.log('listening on *:' + port);
});