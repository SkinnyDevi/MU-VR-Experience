module.exports = app => {
    const ratings = require("../controllers/rating.controller.js");
    const auth = require("../controllers/auth.js");

    var router = require("express").Router();

    //router.post("/", auth.isAuthenticated, ratings.create);

    router.get("/", auth.isAuthenticated, ratings.findAll);

    router.get("/rating/:id", auth.isAuthenticated, ratings.findOne);

    router.get("/clip/:id", auth.isAuthenticated, ratings.findAllClipRatings);

    router.get("/user/:id", auth.isAuthenticated, ratings.findAllUserRatings);

    app.use('/ratings', router);
};