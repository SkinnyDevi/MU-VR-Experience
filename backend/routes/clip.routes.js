module.exports = app => {
	const clips = require("../controllers/clip.controller.js");
	const auth = require("../controllers/auth.js");

	var router = require("../websockets/ws_clips.js").getWSRouter(app);

	router.post("/", auth.isAuthenticated, clips.create);

	router.get("/", auth.isAuthenticated, clips.findAll);

	router.get("/clip/:id", auth.isAuthenticated, clips.findOne);

	router.put("/clip/:id", auth.isAuthenticated, clips.update);

	router.delete("/clip/:id", auth.isAuthenticated, clips.delete);

	app.use('/clips', router);
};