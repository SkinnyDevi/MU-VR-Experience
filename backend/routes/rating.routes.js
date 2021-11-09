module.exports = app => {
    const ratings = require("../controllers/rating.controller.js");
    const auth = require("../controllers/auth.js");

    var router = require("express").Router();

    router.post("/", auth.isAuthenticated, ratings.create);

    router.get("/", auth.isAuthenticated, ratings.findAll);

    router.get("/rating/clip/:id", auth.isAuthenticated, ratings.findAllClipRatings);

    router.get("/rating/user/:id", auth.isAuthenticated, ratings.findAllUserRatings);

    // TODO: update, delete

    app.use('/ratings', router);
};