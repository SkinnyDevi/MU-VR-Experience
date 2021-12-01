module.exports = (sequelize, Sequelize) => {
	const Rating = sequelize.define("rating", {
		rating_id: {
			type: Sequelize.INTEGER,
			primaryKey: true,
			autoIncrement: true
		},
		user_id: {
			type: Sequelize.INTEGER,
			references: {
				model: 'user',
				key: 'user_id',
			}
		},
		clip_id: {
			type: Sequelize.INTEGER,
			references: {
				model: 'clip',
				key: 'clip_id'
			}
		},
		rating: {
			type: Sequelize.ENUM('Liked', 'Regular', 'Disliked')
		}
	});

	return Rating;
}