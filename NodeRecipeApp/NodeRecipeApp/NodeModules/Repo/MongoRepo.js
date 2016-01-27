var exports = module.exports = {};
var mongoose = require('mongoose');
var schema;
var recipeBook;
var Recipe;
//Connection
var db = mongoose.connection;
db.on("error", console.error.bind(console, 'connection error:'));
db.once('open', function (callback) {
    schema = mongoose.Schema;
    recipeBook = new schema({
        Id: Number,
        Name: String,
        Difficulty: String,
        TypeOfDish: String,
        Hours: Number,
        Minutes: Number,
        Ingredients: [],
        Instructions: [],
        Image: String
    });
    Recipe = mongoose.model('recipeBook', recipeBook);
    console.log("connected to mongo");
});
mongoose.connect("mongodb://localhost/recipe");
//
exports.storeRecipe = function (passedRecipe, callback) {
    
    var model = new Recipe({});
    model._id = mongoose.Types.ObjectId();
    model.Id = passedRecipe.Id;
    model.Name = passedRecipe.Name;
    model.Difficulty = passedRecipe.Difficulty;
    model.TypeOfDish = passedRecipe.TypeOfDish;
    model.Hours = passedRecipe.Hours;
    model.Minutes = passedRecipe.Minutes;
    model.Ingredients = passedRecipe.Ingredients;
    model.Instructions = passedRecipe.Instructions;
    model.Image = passedRecipe.Image;

model.save(function (err, model) {
    if (err) {
        console.log(err);
        callback(false);
    }
    else {
        console.log(model);
        callback(true);
    }
});
}

exports.setEdit = function (changes, callback) {
    Recipe.findById(changes._id, function (err, model) {
        if (err) {
            console.log(err);
            callback(false);
        }
        else {
            model.Id = changes.Id;
            model.Name = changes.Name;
            model.Difficulty = changes.Difficulty;
            model.TypeOfDish = changes.TypeOfDish;
            model.Hours = changes.Hours;
            model.Minutes = changes.Minutes;
            model.Ingredients = changes.Ingredients;
            model.Instructions = changes.Instructions;
            model.Image = changes.Image;

            model.save(function (err, update){
                if (err) {
                    console.log(err);
                    callback(false);
                }
                else {
                    console.log(update);
                    callback(true);
                }
            })
        }
    })
}

exports.getRecipe = function (query, callback) {
    Recipe.findOne({Name: query}, function (err, recipe) {
        if (err) {
            console.log(err);
            callback(false);
        }
        else {
            callback(recipe);
        }
    });
}

exports.getAllRecipes = function (callback) {
    Recipe.find(function (err, recipes) { 
        if (err) {
            console.log(err);
            callback(false);
        }
        else {
            callback(recipes);
        }
    });
    
}

exports.delete = function (name, callback) {
    Recipe.findOneAndRemove(name, function (err, deleted) {
        if (err) {
            console.log(err);
            callback(false);
        }
        else if (deleted == null){
            console.log(deleted);
            callback(false);
        }
        else {
            callback(true);
        }
    })    
}

exports.searchForRecipes = function (name, callback) {
    
    //Recipe.find({Name:{$regex:/name/i}}, function (err, recipe){
    
    //    if (err) {
    //        console.log(err);
    //        callback(false);
    //    }
    
    //    else {
    //        callback(recipe);
    //    }
    //})
    
    var query = Recipe.find(name);
    query.$where("this.Name == name")
    .select("Recipe")
    .exec(function (err, result) {
        if (err) {
            console.log(err);
            callback(false);
        }
        else {
            callback(result);
        }
    });
}