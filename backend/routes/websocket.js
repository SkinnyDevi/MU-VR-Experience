var express = require("express");
var router = express.Router();

router.ws("/", (ws, req) => {
	console.log("Connected a new client!");
	ws.send("You connected successfully to the " + req.baseUrl + " socket!");

	ws.on('message', msg => {
		ws.send("Message received: " + msg);
		console.log("New message received: " + msg);
	});
});

module.exports = router;