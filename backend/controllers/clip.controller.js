const db = require("../models");
const ClipService = require("../services/clip.service");
const Clip = db.clip;
const utils = require("../utils");

let clipService = new ClipService();

function handleResponse(serviceRes, res) {
	if (!serviceRes.error) res.send(serviceRes);
	else res.status(500).send(serviceRes);
}

exports.create = (req, res) => {
	if (!req.body.clip_name || !req.body.clip_duration) {
		return res.status(400).send({
			message: "Clip information is empty"
		});
	}

	let clip = {
		clip_id: 0,
		clip_name: req.body.clip_name,
		clip_duration: req.body.clip_duration,
		clip_trailer_img: req.body.clip_trailer_img
	}

	clipService.createClip(clip).then(r => {
		console.log("\n", r, "\n")
		handleResponse(r, res);
	});
}

exports.findAll = (req, res) => {
	clipService.findAll().then(r => {
		handleResponse(r, res);
	});
}

exports.findOne = (req, res) => {
	const id = req.params.id;
	Clip.findByPk(id).then(data => {
		res.send(data);
	}).catch(err => {
		res.status(500).send({
			message: "Error retrieving the clip information of ID: " + id
		});
	});
}

exports.update = (req, res) => {
	const id = req.params.id;

	Clip.findOne({ where: { clip_id: id } }).then(data => {
		if (data) {
			Clip.update(req.body, { where: { clip_id: id } }).then(code => {
				if (code == 1) {
					res.send({
						message: "Clip was updated successfully."
					});
				} else {
					res.send({
						message: "Cannot update the clip with id: " + id + "Maybe the is no clip or the request body is empty."
					});
				}
			}).catch(err => {
				res.status(500).send({
					message: "There was an error updating the clip: " + err.message
				});
			});
		} else {
			return res.status(404).send("No valid clip was found for update.");
		}
	}).catch(err => {
		res.status(500).send({
			message: "There was an error updating the clip: " + err.message
		})
	});
}

exports.delete = (req, res) => {
	const id = req.params.id;

	Clip.destroy({ where: { clip_id: id } }).then(code => {
		if (code == 1) {
			res.send({
				message: "Clip was deleted successfully."
			});
		} else {
			res.status(404).send({
				message: "Cannot delete the clip with id: " + id + ". Maybe the clip was not found."
			});
		}
	}).catch(err => {
		res.status(500).send({
			message: "There was an error while deleting the clip: " + err.message
		});
	});
}