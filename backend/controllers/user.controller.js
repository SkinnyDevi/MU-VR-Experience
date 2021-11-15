const db = require("../models");
const User = db.user;
const utils = require("../utils");
const bcrypt = require('bcryptjs');

// Create and Save a new User
exports.create = (req, res) => {
    if (!req.body.password || !req.body.username) {
        res.status(400).send({
            message: "User information is empty."
        });
        return;
    }

    // Create a User
    let user = {
        email: req.body.username,
        password: req.body.password,
        username: utils.generateUsername(req.body.username),
        isAdmin: req.body.isAdmin ? req.body.isAdmin : false
    };

    User.findOne({ where: { username: user.username } }).then(data => {
        if (data) {
            const result = bcrypt.compareSync(user.password, data.password);
            if (!result) return res.status(401).send('Password is not valid.');

            const token = utils.generateToken(data);
            const userObj = utils.getCleanUser(data);

            return res.json({ user: userObj, access_token: token });
        }

        user.password = bcrypt.hashSync(req.body.password);

        // User not found. Save new User in the database
        User.create(user).then(data => {
            const token = utils.generateToken(data);
            const userObj = utils.getCleanUser(data);

            return res.json({ user: userObj, access_token: token });
        }).catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while creating the user."
            });
        });
    }).catch(err => {
        res.status(500).send({
            message: err.message || "Some error occurred while retrieving a user."
        });
    });
};

// Retrieve all Users from the database.
exports.findAll = (req, res) => {
    User.findAll().then(data => {
        res.send(data);
    }).catch(err => {
        res.status(500).send({
            message: err.message || "Some error occurred while retrieving all users."
        });
    });
};

// Find a single User with an id
exports.findOne = (req, res) => {
    const id = req.params.id;
    User.findByPk(id).then(data => {
        res.send(data);
    }).catch(err => {
        res.status(500).send({
            message: "Error retrieving User with id: " + id
        });
    });
};

// Update a User by the id in the request
exports.update = (req, res) => {
    const id = req.params.id;

    User.findOne({ where: { user_id: id } }).then(data => {
        if (data) {
            const result = bcrypt.compareSync(req.body.password, data.password);
            if (!result) {
                req.body.password = bcrypt.hashSync(req.body.password);
            } else {
                req.body.password = data.password;
            }
            User.update(req.body, { where: { user_id: id } }).then(num => {
                if (num == 1) {
                    res.send({
                        message: "User was updated successfully."
                    });
                } else {
                    res.send({
                        message: `Cannot update User with id: ${id}. Maybe the user was not found or req.body is empty!`
                    });
                }
            }).catch(err => {
                res.status(500).send({
                    message: "Error updating User with id: " + id
                });
            });
        } else {
            return res.status(401).send("No valid user was found for update.")
        }
    }).catch(err => {
        res.status(500).send({
            message: err.message || "Some error occurred while retrieving a user for update."
        });
    });
};

// // Delete a User with the specified id in the request
exports.delete = (req, res) => {
    const id = req.params.id;

    User.destroy({
        where: { user_id: id }
    }).then(num => {
        if (num == 1) {
            res.send({
                message: "User was deleted successfully!"
            });
        } else {
            res.send({
                message: `Cannot delete User with id: ${id}. Maybe the user was not found!`
            });
        }
    }).catch(err => {
        res.status(500).send({
            message: "Could not delete User with id: " + id
        });
    });
};

// Find all admins
exports.findAllAdmin = (req, res) => {
    User.findAll({ where: { isAdmin: true } }).then(data => {
        res.send(data);
    }).catch(err => {
        res.status(500).send({
            message: err.message || "Some error occurred while retrieving admins."
        });
    });
};

// Find user by username and password
exports.findUserByEmailAndPassword = (req, res) => {
    const email = req.body.email;
    const pwd = req.body.password;

    User.findOne({ where: { email: email, password: pwd } }).then(data => {
        res.send(data);
    }).catch(err => {
        res.status(500).send({
            message: err.message || "Some error occurred while retrieving a certain user."
        });
    });
};