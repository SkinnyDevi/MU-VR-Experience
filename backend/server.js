require('dotenv').config();

const jwt = require('jsonwebtoken');
const express = require('express');
const cors = require('cors');
const bodyParser = require('body-parser');
const db = require("./models");

const app = express();
const port = process.env.PORT || 6996;

app.use(cors());

app.use(bodyParser.json());

app.use(bodyParser.urlencoded({ extended: true }));


try {
    db.sequelize.authenticate();
    console.log("Connection has been established successfully.");
} catch (error) {
    console.error("Unable to connect to the database: " + error);
}


app.use(function(req, res, next) {
    var token = req.headers['authorization'];
    if (!token) return next();

    if (req.headers.authorization.indexOf('Basic ') === 0) {
        const base64Credentials = req.headers.authorization.split(' ')[1];
        const credentials = Buffer.from(base64Credentials, 'base64').toString('ascii');
        const [username, password] = credentials.split(':');

        req.body.username = username;
        req.body.password = password;

        return next();
    }

    token = token.replace('Bearer ', '');
    // .env should contain a line like JWT_SECRET=V3RY#1MP0RT@NT$3CR3T#
    jwt.verify(token, process.env.JWT_SECRET, function(err, user) {
        if (err) {
            return res.status(401).json({
                error: true,
                message: "Invalid user."
            });
        } else {
            req.user = user; //set to user for other routes
            req.token = token;
            next();
        }
    });
});

require("./routes/user.routes")(app);

app.listen(port, () => {
    console.log('Server listening on port: ' + port);
});