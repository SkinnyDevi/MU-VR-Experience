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
}

module.exports = ReportController;