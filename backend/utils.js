var jwt = require('jsonwebtoken'); // generate token using secret from process.env.JWT_SECRET

// generate token and return it
function generateToken(user) {
    //1. Don't use password and other sensitive fields
    //2. Use the information that are useful in other parts
    if (!user) return null;

    var u = {
        user_id: user.user_id,
        username: user.username,
        email: user.email,
        password: user.password,
        isAdmin: user.isAdmin,
    };

    // .env should contain a line like JWT_SECRET=V3RY#1MP0RT@NT$3CR3T#
    return jwt.sign(u, process.env.JWT_SECRET, {
        expiresIn: 60 * 60 * 24 // expires in 24 hours
    });
}

// return basic user details
function getCleanUser(user) {
    if (!user) return null;

    return {
        user_id: user.user_id,
        username: user.username,
        email: user.email,
        password: user.password,
        isAdmin: user.isAdmin,
    };
}

function getCleanClip(clip) {
    if (!clip) return null;

    return {
        clip_id: clip.clip_id,
        clip_name: clip.clip_name,
        clip_duration: clip.clip_duration,
        clip_trailer_img: clip.clip_trailer_img
    };
}

function getCleanRating(rating) {
    if (!rating) return null;

    return {
        rating_id: rating.rating_id,
        user_id: rating.user_id,
        clip_id: rating.clip_id,
        rating: rating.rating
    }
}

function generateUsername(email_str) {
    let username = "";
    username += email_str.split("@")[0];
    username = username.replace(".", "_");
    return username.toLowerCase();
}

module.exports = {
    generateToken,
    getCleanUser,
    getCleanClip,
    getCleanRating,
    generateUsername
}