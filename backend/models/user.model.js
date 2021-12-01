module.exports = (sequelize, Sequelize) => {
	const User = sequelize.define("user", {
		user_id: {
			type: Sequelize.INTEGER,
			primaryKey: true,
			autoIncrement: true
		},
		password: {
			type: Sequelize.STRING
		},
		email: {
			type: Sequelize.STRING
		},
		username: {
			type: Sequelize.STRING
		},
		isAdmin: {
			type: Sequelize.BOOLEAN
		}
	});

	return User;
};