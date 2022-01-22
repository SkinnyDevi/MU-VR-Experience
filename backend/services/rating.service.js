const db = require("../models");
const Rating = db.rating;

class RatingService {
	async createRating(rating) {
		return Rating.create(rating);
	}

	async findAll(query) {
		if (query) return Rating.findAll({ where: query});
		else return Rating.findAll();
	}

	async findOne(searchQuery) {
		return Rating.findOne({ where: searchQuery });
	}

	async deleteRating(id) {
		return Rating.destroy({ where: { rating_id: id } });
	}

	async updateRating(updatedRating, id) {
		return Rating.update(updatedRating, { where: { rating_id: id } });
	}
}

module.exports = RatingService;