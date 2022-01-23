const RatingService = require("./rating.service");
const ClipService = require("./clip.service");
const MailingService = require("./email.service");

const jsreport = require("@jsreport/jsreport-core")();
jsreport.use(require('@jsreport/jsreport-chrome-pdf')());
jsreport.use(require('@jsreport/jsreport-jsrender')());
const fs = require("fs").promises;

class ReportService {
	ratingService = new RatingService();
	clipService = new ClipService();
	mailingService = new MailingService();

	async createRatingPDF() {
		const ratings = await this.ratingService.findAll();
		const ratingData = { "ratings": ratings };

		let chartData = await this.computeChartInfo(ratings);

		!this.jsreportInstance ? this.jsreportInstance = await jsreport.init() : null;
		const renderResult = await jsreport.render({
			template: {
				content: this.returnReportTemplate(chartData[0], chartData[1], chartData[2]),
				engine: 'jsrender',
				recipe: 'chrome-pdf',
			},
			data: JSON.stringify(ratingData)
		});

		/* // Returns an HTML file
		await fs.writeFile('jsreport/rendered-report/report.html', renderResult.content);

		return __dirname + "/../jsreport/rendered-report/report.html";
		*/
		
		
		await fs.writeFile('jsreport/rendered-report/report.pdf', renderResult.content);
		let file = await fs.readFile(__dirname+"/../jsreport/rendered-report/report.pdf");

		return file;
	}

	async sendPdfThroughEmail(senderEmail) {
		const ratings = await this.ratingService.findAll();
		const ratingData = { "ratings": ratings };

		let chartData = await this.computeChartInfo(ratings);

		!this.jsreportInstance ? this.jsreportInstance = await jsreport.init() : null;
		const renderResult = await jsreport.render({
			template: {
				content: this.returnReportTemplate(chartData[0], chartData[1], chartData[2]),
				engine: 'jsrender',
				recipe: 'chrome-pdf',
			},
			data: JSON.stringify(ratingData)
		});

		await fs.writeFile('jsreport/rendered-report/report.pdf', renderResult.content);

		var today = new Date();
		var date = today.getDate() + '/' + (today.getMonth()+1) + '/' + today.getFullYear();
		var time = today.getHours() + ":" + today.getMinutes();

		let bodyHtml =  `<h2>Informe generado en fecha: ${date} a las ${time}</h2>`

		return this.mailingService.sendReport(senderEmail, "Informe de Valoraciones Por Video", bodyHtml, __dirname + "/../jsreport/rendered-report/report.pdf");
	}

	// Helper Functions \/ Below
	async computeChartInfo(ratingsJson) {
		let ratings = ratingsJson;

		let ratingsPerVideo = [];
		let totalVideos = [];
		let videoLabels = [];
		let countValues = [];

		// Add video counts to ratingsPerVideo
		for (let rating of ratings) {
			if (ratingsPerVideo.length == 0) {
				ratingsPerVideo.push({
					video_id: rating.clip_id,
					count: 0
				});
				let clip = await this.clipService.findOne({ clip_id: rating.clip_id});
				videoLabels.push("\""+clip.clip_name+"\"");
				totalVideos.push(rating.clip_id);
			} else {
				let addVideo = true;
				for (let i = 0; i < ratingsPerVideo.length; i++) {
					if (ratingsPerVideo[i].video_id == rating.clip_id) {
						addVideo = false;
						break;
					}
				}
				if (addVideo) {
					ratingsPerVideo.push({
						video_id: rating.clip_id,
						count: 0
					});
					let clip = await this.clipService.findOne({ clip_id: rating.clip_id});
					videoLabels.push("\""+clip.clip_name+"\"");
					totalVideos.push(rating.clip_id);
				}
			}
		}

		// Add rating count for each video
		for (let rating of ratings) {
			for (let i = 0; i < ratingsPerVideo.length; i++) {
				if (ratingsPerVideo[i].video_id == rating.clip_id) ratingsPerVideo[i].count++;
			}
		}

		// Add values for chart
		for (let i = 0; i < ratingsPerVideo.length; i++) {
			countValues.push(ratingsPerVideo[i].count);
		}

		return [totalVideos, countValues, videoLabels];
	}

	returnReportTemplate(totalNVideos, chartDataCountValues, chartVideoLabels) {
		return `
		<html>
		<head>
				<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.4/Chart.js"></script>
		</head>
		<body>
				<style>
						body {
							height: 100%;
						}
	
						h1 {
							font-size: 40px;
							text-align: center;
							padding-top: 20px;
							color: rgb(102, 102, 102);
						}
						
						body {
							margin: 0;
							height: 100%;
							//background-image: linear-gradient(45deg, #ffffff, #56aaff), linear-gradient(#ffffff, #56aaff);
							//background-size: 100% 20px, 100%;
							font-family: sans-serif;
							font-weight: 100;
						}
						
						.container {
							position: absolute;
							left: 13%; // 22.5% for website
						}
						
						table {
							width: 600px;
							border-collapse: collapse;
							overflow: hidden;
							box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
						}
						
						th,
						td {
							padding: 15px;
							background-color: rgba(0, 0, 0, 0.2);
							color: #000000; // #ffffff for html
						}
						
						th {
							text-align: left;
						}
						
						thead>th {
							background-color: #55608f;
						}
						
						tbody>tr:hover {
							background-color: rgba(255, 255, 255, 0.3);
						}
						
						tbody>td {
							position: relative;
						}
						
						tbody>td:hover::before {
							content: "";
							position: absolute;
							left: 0;
							right: 0;
							top: -9999px;
							bottom: -9999px;
							background-color: rgba(255, 255, 255, 0.2);
							z-index: -1;
						}
				</style>
				<header>
					<h1>Listado de Valoraciones</h1>
				</header>
				<div class="container">
						<table>
							<thead>
								<tr>
									<th>ID Valoración</th>
									<th>ID Video</th>
									<th>ID Usuario</th>
									<th>Valoración</th>
								</tr>
							</thead>
							<tbody>
								{{for ratings}}
								<tr>
									<td>{{:rating_id}}</td>
									<td>{{:clip_id}}</td>
									<td>{{:user_id}}</td>
									<td>{{:rating}}</td>
								</tr>
								{{/for}}
							</tbody>
						</table>
						<canvas id="ratingsPerVideo" style="margin-top: 50px"></canvas>
					</div>
						<script>
								var xValues = [${totalNVideos}];
								var yValues = [${chartDataCountValues}];
								var xLabels = [${chartVideoLabels}];
								var barColors = ["purple", "pink","blue","red","brown","orange","green"];
	
								new Chart("ratingsPerVideo", {
									type: "pie",
									data: {
										labels: xLabels,
										datasets: [{
											backgroundColor: barColors,
											data: yValues
										}]
									},
									options: {
										title: {
											display: true,
											text: "Cantidad de Valoraciones por Video",
											fontSize: 35,
											color: "black"
										},
										animation: {
											duration: 0
										}
									}
								});
						</script>
		</body>
		</html>`;
	}
}

module.exports = ReportService;