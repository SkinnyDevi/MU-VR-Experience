require('dotenv').config();
const transporter = require("../config/mailing");

class MailingService {
	async sendReport(senderEmail, emailSubject, emailHtmlBody, filePath) {
		try {
			await transporter.sendMail({
				from: '"Manos Unidas Virtual Reality Experience" <' + process.env.emailApiName + '>',
				to: senderEmail,
				subject: emailSubject,
				text: "",
				html: emailHtmlBody,
				attachments: [
					{ path: filePath }
				],
			});
		} catch (err) {
			console.error("There was an error when sending the email: " + err.message);
		}
	}
}

module.exports = MailingService;