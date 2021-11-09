module.exports = (sequelize, Sequelize) => {
    const User = sequelize.define("user", {
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

    User.associate = function(models) {
        User.hasOne(models.rating, {
            onDelete: "RESTRICT",
            onUpdate: "CASCADE",
            foreignKey: 'userId'
        });
    }

    return User;
};