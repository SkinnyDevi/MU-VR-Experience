module.exports = (sequelize, Sequelize) => {
    const Rating = sequelize.define("rating", {
        rating: {
            type: Sequelize.ENUM('Liked', 'Disliked')
        }
    });

    Rating.associate = function(models) {
        Rating.belongsTo(models.user);
        Rating.belongsTo(models.clip);
    }

    return Rating;
}