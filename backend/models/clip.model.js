module.exports = (sequelize, Sequelize) => {
    const Clip = sequelize.define("clip", {
        clip_name: {
            type: Sequelize.STRING
        },
        clip_duration: {
            type: Sequelize.STRING
        },
        clip_trailer_img: {
            type: Sequelize.TEXT
        }
    });

    Clip.associate = function(models) {
        Clip.hasOne(models.rating, {
            onDelete: "RESTRICT",
            onUpdate: "CASCADE",
            foreignKey: 'clipId'
        });
    }

    return Clip;
}