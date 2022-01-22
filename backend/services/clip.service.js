const db = require("../models");
const Clip = db.clip;
const utils = require("../utils");

class ClipService {
	async createClip(clip) {
		let exists;
		
		try {
			exists = await Clip.findOne({ where: { clip_name: clip.clip_name } });
		} catch (err) {
			return { error : "Couldn't find an existing clip: " + err.message}
		}

		if (exists) return utils.getCleanClip(exists);
		else {
			Clip.create(clip).then(createData => {
				return utils.getCleanClip(createData);
			}).catch(err => {
				return { error : "Couldn't find an existing clip: " + err.message}
			});
		} 
	}

	async findAll() {
		let allClips = Clip.findAll();

		if (allClips) return allClips;
		else return { error: "Some error occurred while retrieving all clips: " + err.message};
	}
}

module.exports = ClipService;