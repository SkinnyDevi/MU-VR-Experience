const express = require("express");
const reportingApp = express();

exports.jsreport_init = (mainApp, server) => {
	mainApp.use("/reporting", reportingApp);

	const jsreportInstance = require("jsreport")({
		extensions: {
			express: { app: reportingApp, server: server }
		},
		appPath: "/reporting"
	});

	jsreportInstance.init().then(() => {
		console.log("JSReport Server Started");
		this.hasInitialized = true;
	}).catch(e => {
		console.error("JSReport Error: \n" + e);
	});
}