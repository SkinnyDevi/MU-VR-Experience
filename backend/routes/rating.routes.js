module.exports = app => {
	const ratings = require("../controllers/rating.controller.js");
	const auth = require("../controllers/auth.js");

	var router = require("express").Router();

	router.get("/", auth.isAuthenticated, ratings.findAll);

	router.get("/rating/:id", auth.isAuthenticated, ratings.findOne);

	router.get("/submitted_rating/exists/:user_id/:clip_id", auth.isAuthenticated, ratings.checkRatingExists);

	router.get("/by_type/:clip_id", auth.isAuthenticated, ratings.returnClipRatingTypes);

	router.put("/rating/", auth.isAuthenticated, ratings.update);

	router.delete("/rating/:id", auth.isAuthenticated, ratings.delete);

	router.get("/clip/:id", auth.isAuthenticated, ratings.findAllClipRatings);

	router.get("/user/:id", auth.isAuthenticated, ratings.findAllUserRatings);

	app.use('/ratings', router);
};