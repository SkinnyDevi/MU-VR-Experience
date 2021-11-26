const db = require("../models");
const Clip = db.clip;

exports.create = (req, res) => {}

exports.findAll = (req, res) => {
    Clip.findAll().then(data => {
        res.send(data)
    }).catch(err => {
        res.status(500).send({
            message: "Some error occurred while retrieving all clips: " + err.message
        })
    })
}

exports.findOne = (req, res) => {
    const id = req.params.id;
    Clip.findByPk(id).then(data => {
        res.send(data);
    }).catch(err => {
        res.status(500).send({
            message: "Error retrieving the clip information of ID: " + id
        })
    })
}

exports.update = (req, res) => {}
exports.delete = (req, res) => {}