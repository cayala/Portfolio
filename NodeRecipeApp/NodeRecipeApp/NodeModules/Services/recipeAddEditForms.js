var exports = module.exports = {};
var repo = require("../Repo/MongoRepo.js");

exports.add = function (req, recipes, callback) {
    
    var body = '';
    req.on('data', function (data) {
        
        body += data.toString();
    });
    
    req.on('end', function (err) {
        
        var recipe = JSON.parse(body);
        recipe.Id = recipes.length;
        recipe.Name = recipe.Name.toLowerCase();
        repo.storeRecipe(recipe, function (saved) { 
            callback(saved);
        });
    })
};

exports.getRecipe = function (req, recipes, callback) {
    
    var body = req.query.Name;
    if (body == undefined) {
        callback(false);
      
    }
    var name = body.toLowerCase();
    
    if (!name.length < 1) {
        repo.getRecipe(name, function (data) { 
            callback(data);
        })
    }
    else {
        callback(false);
    }
}

exports.edit = function (req, recipes, callback) {
    
    var body = '';
    
    req.on('data', function (data) {
        
        body += data.toString();
    });
    
    req.on('end', function () {
        
        var edittedRecipe = JSON.parse(body);
        repo.setEdit(edittedRecipe, function (data) { 
            callback(data);
        })
        
    
    });
}