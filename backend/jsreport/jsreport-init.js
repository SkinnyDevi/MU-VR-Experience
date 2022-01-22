const express = require("express");
const reportingApp = express();

exports.jsreport_init = (mainApp, server) => {
	mainApp.use("/reporting", reportingApp);

	const jsreport = require("jsreport")({
		extensions: {
			express: {app: reportingApp, server: server}
		},
		appPath: "/reporting"
	});
	
	jsreport.init().then(() => {
		console.log("JSReport Server Started");
	}).catch(e => {
		console.error("JSReport Error: \n" + e);
	});
}