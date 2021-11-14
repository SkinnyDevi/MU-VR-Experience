module.exports = (sequelize, Sequelize) => {
    const User = sequelize.define("user", {
        user_id: {
            type: Sequelize.INTEGER
        },
        password: {
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