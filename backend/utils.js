var jwt = require('jsonwebtoken');

function getCleanUser(user) {
	if (!user) return null;

	return {
		user_id: user.user_id,
		username: user.username,
		email: user.email,
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
	};
}

function generateToken(user) {
	if (!user) return null;

	var u = getCleanUser(user);

	return jwt.sign(u, process.env.JWT_SECRET, {
		expiresIn: 60 * 60 * 24
	});
}

function generateUsername(email_str) {
	let username = "";
	username += email_str.split("@")[0];
	username = username.replace(".", "_");
	return username.toLowerCase();
}

function generateSplitRatingTypeJSON(ratingJSON) {
	if (!ratingJSON) return null;

	let splitType = {
		Liked: [],
		Regular: [],
		Disliked: []
	}

	for (let rating of ratingJSON) {
		switch (rating.rating) {
			case "Liked":
				splitType.Liked.push(rating);
				break;
			case "Regular":
				splitType.Regular.push(rating);
				break;
			case "Disliked":
				splitType.Disliked.push(rating);
				break;
		}
	}

	return splitType;
}

module.exports = {
	generateToken,
	getCleanUser,
	getCleanClip,
	getCleanRating,
	generateUsername,
	generateSplitRatingTypeJSON
}