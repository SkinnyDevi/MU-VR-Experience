module.exports = app => {
	const RatingController = require("../controllers/rating.controller");
	const ReportController = require("../controllers/reports.controller");
	const auth = require("../controllers/auth.js");

	var router = require("../websockets/ws_ratings.js").getWSRouter(app);
	const ratingController = new RatingController();
	const reportController = new ReportController();

	router.get("/", auth.isAuthenticated, ratingController.findAll);

	router.get("/rating/:id", auth.isAuthenticated, ratingController.findOne);

	router.get("/submitted_rating/exists/:user_id/:clip_id", auth.isAuthenticated, ratingController.checkRatingExists);

	router.get("/by_type/:clip_id", auth.isAuthenticated, ratingController.returnClipRatingsTypes);

	router.get("/clip/:id", auth.isAuthenticated, ratingController.findAllClipRatings);

	router.get("/user/:id", auth.isAuthenticated, ratingController.findAllUserRatings);

	router.get("/report", auth.isAuthenticated, reportController.gatherReport);

	router.post("/report", auth.isAuthenticated, reportController.sendReportThroughEmail);

	router.put("/rating/", auth.isAuthenticated, ratingController.updateRating);

	router.delete("/rating/:id", auth.isAuthenticated, ratingController.deleteRating);

	app.use('/ratings', router);
};