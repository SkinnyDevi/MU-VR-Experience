const utils = require("../utils");
const db = require("../models");
const Rating = db.rating;

const express = require("express");

exports.getWSRouter = (expressApp) => {
	const expressWs = require("express-ws")(expressApp);
	var router = express.Router();

	router.ws("/", (socket, req) => {
		console.log("Connected a new client!");
		socket.send("You connected successfully to the " + req.baseUrl + " socket!");

		socket.on('message', msg => {
			msg = JSON.parse(msg);
			console.log("Event received: " + msg.event);
			console.log("Info Received: " + msg.payload.clipId);

			switch (msg.event) {
				case "newRating":
					Rating.findAll({ where: { clip_id: msg.payload.clipId } }).then(findData => {
						if (findData) {
							if (Array.from(findData)[0] != undefined) {
								formattedInfo = {
									return_event: "newRatingReturn",
									returned_payload: {
										clip_id: msg.payload.clipId,
										updated_ratings: utils.generateSplitRatingTypeJSON(findData)
									}
								}
								expressWs.getWss("/").clients.forEach(client => {
									client.send(JSON.stringify(formattedInfo));
								});
							} else {
								socket.send(JSON.stringify({
									non_existent: "Failed to find clip. Does it really exist or does it have any submitted rating?"
								}));
							}
						}
					}).catch(err => {
						socket.send(JSON.stringify({
							error: "There was an error retriving clip ratings: " + err.message
						}));
					});
					break;
			}
		});
	});

	return router;
}