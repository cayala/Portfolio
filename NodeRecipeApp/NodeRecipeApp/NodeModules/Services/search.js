var exports = module.exports = {};
var repo = require("../Repo/MongoRepo.js");

exports.recipes = function (req, recipes, callback) {
    repo.getAllRecipes(function (data) {
        callback(data);
    });
    
}

exports.getRecipe = function (req, recipes, callback) {
    
    var body = req.query.query;
    if (body == undefined) {
        callback(false);
    }
    var name = body.toLowerCase();
    var list = [];
    if (!name.length < 1) {
        repo.searchForRecipes(name, function (data) { 
            callback(data);
        });    
        }
}

