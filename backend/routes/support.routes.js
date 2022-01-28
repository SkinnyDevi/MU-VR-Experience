module.exports = app => {
	const express = require("express");
	const HelperSystemsController = require("../controllers/helpersystem.controller");

	var router = express.Router();
	const uhsController = new HelperSystemsController();

	app.use("/support/user", express.static(__dirname + "/../support/user"));

	app.use("/support/developer", express.static(__dirname + "/../support/developer"));

	router.get("/user", uhsController.getUserHelper);

	router.get("/developer", uhsController.getDeveloperHelper);

	app.use('/support', router);
}

