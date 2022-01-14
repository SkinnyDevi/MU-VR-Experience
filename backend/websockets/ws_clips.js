const db = require("../models");
const Clip = db.clip;

const express = require("express");

exports.getWSRouter = (expressApp) => {
	const expressWs = require("express-ws")(expressApp)
	var router = express.Router();

	router.ws("/", (socket, req) => {
		console.log("Connected a new client!");
		socket.send("You connected successfully to the " + req.baseUrl + " socket!");

		socket.on('message', msg => {
			msg = JSON.parse(msg);
			console.log("Event received: " + msg.event);

			switch (msg.event) {
			}
		});
	});

	return router;
}