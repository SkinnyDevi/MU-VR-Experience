const db = require("../models");
const Rating = db.rating;

exports.create = (req, res) => {}

exports.findAll = (req, res) => {
    Rating.findAll().then(data => {
        res.send(data);
    }).catch(err => {
        res.status(500).send({
            message: "An error ocurred while retrieving ratings: " + err.message
        });
    });
}

exports.findOne = (req, res) => {
    const id = req.params.id;
    Rating.findByPk(id).then(data => {
        res.send(data);
    }).catch(err => {
        res.status(500).send({
            message: "Error while retrieving rating with ID: " + id
        });
    });
}

exports.findAllUserRatings = (req, res) => {
    const user_id = req.params.id;
    Rating.findAll({ where: { user_id: user_id } }).then(data => {
        res.send(data);
    }).catch(err => {
        res.status(500).send({
            message: "Error while retrieving rating for user with ID: " + id
        });
    });
}
exports.findAllClipRatings = (req, res) => {
    const clip_id = req.params.id;
    Rating.findAll({ where: { clip_id: clip_id } }).then(data => {
        res.send(data);
    }).catch(err => {
        res.status(500).send({
            message: "Error while retrieving rating for clip with ID: " + id
        });
    });
}

exports.update = (req, res) => {}
exports.delete = (req, res) => {}