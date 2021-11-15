module.exports = (sequelize, Sequelize) => {
    const Clip = sequelize.define("clip", {
        clid_id: {
            type: Sequelize.INTEGER,
            primaryKey: true
        },
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

    return Clip;
}