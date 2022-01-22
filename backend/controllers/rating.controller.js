const db = require("../models");
const Rating = db.rating;
const utils = require("../utils");
const RatingService = require("../services/rating.service");


class RatingController {
	ratingService = new RatingService();

	updateRating = (req, res) => {
		const rBody = req.body;

		this.ratingService.findOne({ user_id: rBody.user_id, clip_id: rBody.clip_id }).then(findData => {
			if (findData) {
				this.ratingService.updateRating(rBody, findData.rating_id).then(code => {
					if (code == 1) {
						res.send({
							message: "Rating was updated successfully."
						});
					} else {
						res.status(403).send({
							error: "Cannot update the rating with id: " + req.params.id + ". Maybe there is no clip or the request body is empty."
						});
					}
				}).catch(err => {
					res.status(500).send({
						error: "There was an error updating the rating: " + err.message
					});
				});
			} else {
				if (!rBody.user_id || !rBody.clip_id || !rBody.rating) {
					return res.status(400).send({
						error: "Rating information is empty."
					});
				}

				let rating = {
					rating_id: 0,
					user_id: rBody.user_id,
					clip_id: rBody.clip_id,
					rating: rBody.rating
				}

				this.ratingService.createRating(rating).then(createData => {
					this.ratingService.findOne({ user_id: rating.user_id, clip_id: rating.clip_id }).then(sendData => {
						createData.rating_id = sendData.rating_id;
						res.send(utils.getCleanRating(createData));
					}).catch(err => {
						res.status(500).send({
							error: "Couldn't retrieve newly created rating: " + err.message
						});
					});
				}).catch(err => {
					res.status(500).send({
						error: "Couldn't create a new rating: " + err.message
					});
				});
			}
		}).catch(err => {
			res.status(500).send({
				error: "There was an error updating the rating: " + err.message
			});
		});
	}

	findAll = (req, res) => {
		this.ratingService.findAll().then(findData => {
			res.send(findData);
		}).catch(err => {
			res.status(500).send({
				error: "An error ocurred while retrieving ratings: " + err.message
			});
		});
	}

	findOne = (req, res) => {
		this.ratingService.findOne({ rating_id: req.params.id }).then(findData => {
			if (findData) res.send(findData);
			else {
				res.status(400).send({
					error: "No rating was found for clip ID (" + req.params.id + ")"
				});
			}
		}).catch(err => {
			res.status(500).send({
				error: "Error while retrieving rating with ID (" + req.params.id + "): " + err.message
			});
		});
	}

	findAllUserRatings = (req, res) => {
		this.ratingService.findAll({ user_id: req.params.id }).then(findData => {
			res.send(findData);
		}).catch(err => {
			res.status(500).send({
				error: "Error while retrieving rating for user with ID (" + req.params.id + "): " + err.message
			});
		});
	}

	findAllClipRatings = (req, res) => {
		this.ratingService.findAll({ clip_id: req.params.id }).then(findData => {
			res.send(findData);
		}).catch(err => {
			res.status(500).send({
				error: "Error while retrieving rating for user with ID (" + req.params.id + "): " + err.message
			});
		});
	}

	returnClipRatingsTypes = (req, res) => {
		this.ratingService.findAll({ clip_id: req.params.clip_id }).then(findData => {
			if (findData) {
				if (Array.from(findData)[0] != undefined) {
					res.send(utils.generateSplitRatingTypeJSON(findData));
				} else {
					res.status(404).send({
						message: "Failed to find clip. Does it really exist or does it have any submitted rating?"
					});
				}
			} else {
				res.status(404).send({
					error: "No ratings were found for the clip with ID (" + req.params.clip_id + ")"
				});
			}
		}).catch(err => {
			res.status(500).send({
				message: "There was an error retriving clip ratings: " + err.message
			});
		});
	}

	checkRatingExists = (req, res) => {
		this.ratingService.findOne({ user_id: req.params.user_id, clip_id: req.params.clip_id }).then(findData => {
			if (findData) {
				res.send({
					message: "Rating exists.",
					rating_id: findData.rating_id
				});
			} else {
				res.status(404).send({
					error: "Rating does not exist."
				});
			}
		}).catch(err => {
			res.status(500).send({
				error: "There was an error checking for the user id: " + req.params.user_id + ", and clip id: " + reqBody.clip_id + ". \nError: " + err.message
			});
		});
	}

	deleteRating = (req, res) => {
		this.ratingService.deleteRating(req.params.id).then(code => {
			if (code == 1) {
				res.send({
					message: "Rating deleted successfully."
				});
			} else {
				res.status(404).send({
					error: "Cannot delete the rating with ID: " + req.params.id + ", Are you sure there is such rating?"
				});
			}
		}).catch(err => {
			res.status(500).send({
				error: "There was an error while deleting the rating: " + err.message
			})
		});
	}
}

module.exports = RatingController;