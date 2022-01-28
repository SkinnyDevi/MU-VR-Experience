const path = require("path");

class HelperSystemsController {
	getUserHelper = (req, res) => {
		res.sendFile(path.resolve(__dirname + "/../support/user/Introduction.html"));
	}

	getDeveloperHelper = (req, res) => {
		res.sendFile(path.resolve(__dirname + "/../support/developer/Introduction.html"));
	}
}

module.exports = HelperSystemsController;