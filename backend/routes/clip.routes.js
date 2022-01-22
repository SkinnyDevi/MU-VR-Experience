module.exports = app => {
	const ClipController = require("../controllers/clip.controller.js");
	const auth = require("../controllers/auth.js");

	var router = require("../websockets/ws_clips.js").getWSRouter(app);
	let clipController = new ClipController();

	router.get("/", auth.isAuthenticated, clipController.findAll);

	router.get("/clip/:id", auth.isAuthenticated, clipController.findOne);

	router.post("/", auth.isAuthenticated, clipController.createClip);
	
	router.put("/clip/:id", auth.isAuthenticated, clipController.updateClip);

	router.delete("/clip/:id", auth.isAuthenticated, clipController.deleteClip);

	app.use('/clips', router);
};