const ClipService = require("../services/clip.service");
const utils = require("../utils");

class ClipController {
	clipService = new ClipService();

	createClip = (req, res) => {
		if (!req.body.clip_name || !req.body.clip_duration) {
			res.status(400).send({
				error: "Clip information is empty!"
			});
		}

		let clip = {
			clip_id: 0,
			clip_name: req.body.clip_name,
			clip_duration: req.body.clip_duration,
			clip_trailer_img: req.body.clip_trailer_img
		}

		this.clipService.findOne({ clip_name: clip.clip_name }).then(findData => {
			if (findData) {
				res.send(utils.getCleanClip(findData));
			} else {
				this.clipService.createClip(clip).then(createData => {
					this.clipService.findOne({ clip_name: clip.clip_name }).then(sendData => {
						createData.clip_id = sendData.clip_id;
						res.send(utils.getCleanClip(createData));
					}).catch(err => {
						res.status(500).send({
							error: "Couldn't retrieve newly created clip: " + err.message
						});
					});
				}).catch(err => {
					res.status(500).send({
						error: "Something happened while creating the clip: " + err.message
					});
				});
			}
		}).catch(err => {
			res.status(404).send({
				error: "Couldn't find an existing clip: " + err.message
			})
		});
	}

	findAll = (req, res) => {
		this.clipService.findAll().then(data => {
			res.send(data);
		}).catch(err => {
			res.status(500).send({
				error: "Some error occurred while retrieving all clips: " + err.message
			});
		});
	}

	findOne = (req, res) => {
		this.clipService.findOne({ clip_id: req.params.id}).then(data => {
			if (data) res.send(data);
			else {
				res.status(400).send({
					error: "No clip was found for clip ID (" + req.params.id + ")"
				});
			}
		}).catch(err => {
			res.status(500).send({
				error: "Error retrieving the clip information of ID (" + req.params.id + "): " + err.message
			});
		});
	}

	updateClip = (req, res) => {
		this.clipService.findOne({ clip_id: req.params.id }).then(findData => {
			if (findData) {
				this.clipService.updateClip(req.body, req.params.id).then(code => {
					if (code == 1) {
						res.send({
							message: "Clip was updated successfully."
						});
					} else {
						res.send({
							error: "Cannot update the clip with id: " + req.params.id + ". Maybe the is no clip or the request body is empty."
						});
					}
				}).catch(err => {
					res.status(500).send({
						error: "There was an error updating the clip: " + err.message
					});
				});
			} else {
				res.status(404).send({
					error: "No valid clip was found for update."
				});
			}
		}).catch(err => {
			res.status(500).send({
				error: "There was an error updating the clip: " + err.message
			});
		});
	}

	deleteClip = (req, res) => {
		this.clipService.deleteClip(req.params.id).then(code => {
			if (code == 1) {
				res.send({
					message: "Clip was deleted successfully."
				});
			} else {
				res.status(404).send({
					error: "Cannot delete the clip with id: " + req.params.id + ". Maybe the clip was not found."
				});
			}
		}).catch(err => {
			res.status(500).send({
				error: "There was an error while deleting the clip: " + err.message
			});
		});
	}
}

module.exports = ClipController;