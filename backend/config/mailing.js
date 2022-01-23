require('dotenv').config()
const nodemailer = require("nodemailer");

transporter = nodemailer.createTransport({
	host: "smtp.gmail.com",
	port: 465,
	secure: true,
	auth: {
		user: process.env.EMAIL_API_NAME,
		pass: process.env.EMAIL_PASSWORD,
	},
});

transporter.verify().then(() => {
	console.log("Ready to send emails.");
});

module.exports = transporter;