const db = require("../models");
const Clip = db.clip;

class ClipService {
	async createClip(clip) {
		return Clip.create(clip);
	}

	async findAll() {
		return Clip.findAll();
	}

	async findOne(searchQuery) {
		return Clip.findOne({ where: searchQuery });
	}

	async deleteClip(id) {
		return Clip.destroy({ where: { clip_id: id } });
	}

	async updateClip(updatedClip, id) {
		return Clip.update(updatedClip, { where: { clip_id: id } });
	}
}

module.exports = ClipService;