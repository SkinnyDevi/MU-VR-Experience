const db = require("../models");
const Rating = db.rating;
const utils = require("../utils");

exports.update = (req, res) => {
	const id = req.params.id;
	const reqBody = req.body

	Rating.findOne({ where: { user_id: reqBody.user_id, clip_id: reqBody.clip_id } }).then(findData => {
		if (findData) {
			Rating.update(reqBody, { where: { rating_id: findData.rating_id } }).then(code => {
				if (code == 1) {
					res.send({
						message: "Rating was updated successfully."
					});
				} else {
					res.send({
						message: "Cannot update the rating with id: " + id + "Maybe the is no clip or the request body is empty."
					});
				}
			}).catch(err => {
				res.status(500).send({
					message: "There was an error updating the rating: " + err.message
				})
			});
		} else {
			if (!reqBody.user_id || !reqBody.clip_id || !reqBody.rating) {
				return res.status(400).send({
					message: "Rating information is empty."
				});
			}

			let rating = {
				rating_id: 0,
				user_id: reqBody.user_id,
				clip_id: reqBody.clip_id,
				rating: reqBody.rating
			}

			Rating.create(rating).then(createData => {
				return res.send(utils.getCleanRating(createData));
			}).catch(err => {
				res.status(500).send({
					message: "Couldn't create a new rating: " + err.message
				});
			});
		}
	}).catch(err => {
		res.status(500).send({
			message: "There was an error updating the rating: " + err.message
		})
	});
}

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

exports.delete = (req, res) => {
	const id = req.params.id;

	Rating.destroy({ where: { rating_id: id } }).then(code => {
		if (code == 1) {
			res.send({
				message: "Rating deleted successfully."
			});
		} else {
			res.status(404).send({
				message: "Cannot delete the rating with ID: " + id + ", Are you sure there is such rating?"
			});
		}
	}).catch(err => {
		res.status(500).send({
			message: "There was an error while deleting the rating: " + err.message
		})
	});
}