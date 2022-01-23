const ReportService = require("../services/reports.service");
const path = require('path');

class ReportController {
	reportService = new ReportService();

	gatherReport = (req, res) => {
		this.reportService.createRatingPDF().then(data => {
			//res.sendFile(path.resolve(data)); // Returns an HTML Site
			res.set('Content-Type', 'application/pdf').send(data); // Returns a PDF Instance
		}).catch(err => {
			res.status(500).send({
				error: "Something ocurred while gathering report: " + err.message
			})
		});
	}


	sendReportThroughEmail = (req, res) => {
		if (!req.body.email) res.status(400).send({
				error: "There is no sender email present."
		});

		this.reportService.sendPdfThroughEmail(req.body.email).then(data => {
			res.send({
				message: "Email has been sent to: " + req.body.email + "!"
			});
		}).catch(err => {
			res.status(500).send({
				error: "Something ocurred while the controller tried to send the email: " + err.message
			});
		});
	}
}

module.exports = ReportController;