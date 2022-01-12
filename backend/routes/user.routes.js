module.exports = app => {
	const users = require("../controllers/user.controller.js");
	const auth = require("../controllers/auth.js");

	var router = require("./websocket.js");

	router.post("/", users.create);

	router.get("/", auth.isAuthenticated, users.findAll);

	router.get("/user/:id", auth.isAuthenticated, users.findOne);

	router.get("/admins", auth.isAuthenticated, users.findAllAdmin);

	router.put("/user/:id", auth.isAuthenticated, users.update);

	router.post("/signin", auth.signin);

	router.delete("/user/:id", auth.isAuthenticated, users.delete);

	app.use('/users', router);
};