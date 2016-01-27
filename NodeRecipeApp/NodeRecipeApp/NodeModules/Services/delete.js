var exports = module.exports = {};
var repo = require("../Repo/MongoRepo.js");
exports.deleteRecipe = function (req, recipes, callback) {
    var deletion = false;
    
    var body = '';
    req.on('data', function (data) {
        body += data.toString();
    });
    req.on('end', function () {
        
        var name = JSON.parse(body);
        
        if (Object.getOwnPropertyNames(name).length < 1) {
            callback(false);
        }

        else if (!name.Name.length < 1 || !name == undefined) {
            repo.delete(name, function (data) { 
                callback(data);
            })
        }
        else {
            callback(false);
        }
    })

}