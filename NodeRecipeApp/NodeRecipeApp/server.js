//using statements
var http = require('http');
var express = require("express");
var app = express();
var fs = require("fs");
var path = require("path");
var search = require("./NodeModules/Services/search.js");
var deleteMod = require('./NodeModules/Services/delete.js');
var addEdit = require('./NodeModules/Services/recipeAddEditForms.js');
var recipes = [];
var dummy = function () {
    var a = {
        Id: 0,
        Name: 'Menudo',
        Difficulty: 'Intermediate',
        TypeOfDish: 'Soup',
        Hours: 2,
        Minutes: 45,
        Ingredients: ['Beef tripe', 'Spices', 'corn'],
        Instructions: ['Add corn and tripe', 'Simmer in spices', 'Serve'],
        Image: 'http://www.bonappetit.com/wp-content/uploads/2013/05/crudo-menudo.jpg'
    };
    var b = {
        Id: 1,
        Name: 'Baked Lobster Tail',
        Difficulty: 'Advanced',
        TypeOfDish: 'Entree',
        Hours: 24,
        Minutes: 0,
        Ingredients: ['Lobster', 'spices', 'lemon'],
        Instructions: ['Bake Lobster', 'Add spicies and lemon', 'Eat'],
        Image: 'http://www.mycraigster.com/images/stories/virtuemart/product/recipe_bakedtails.jpg'
    };
    var c = {
        Id: 2,
        Name: 'Oatmeal',
        Difficulty: 'Beginner',
        TypeOfDish: 'Breakfast',
        Hours: 0,
        Minutes: 30,
        Ingredients: ['Oats', 'milk', 'sugar'],
        Instructions: ['boil milk', 'add oats', 'let cook for 30 min'],
        Image: 'http://greenlitebites.com/resources/2012/20120212_PBandBananaOatmeal4.jpg'
    };
    var d = {
        Id: 3,
        Name: 'French Onion Soup',
        Difficulty: 'beginner',
        TypeOfDish: 'Soup',
        Hours: 3,
        Minutes: 30,
        Ingredients: ['Onions', 'White Cheddar cheese', 'crutons'],
        Instructions: ['Chop onions', 'cook in beef broth till soft', 'broil with cheese and cruton'],
        Image: 'http://brightcove.vo.llnwd.net/d21/unsecured/media/1033249144001/201311/306/1033249144001_2813463860001_video-still-for-video-2808711381001.jpg?pubId=1033249144001'
    };
    recipes.push(a);
    recipes.push(b);
    recipes.push(c);
    recipes.push(d);
}
dummy();
//
//mongo Connection

//
//Static files being served
app.use('/Views', express.static(__dirname + "/Views"));
app.use('/Scripts', express.static(__dirname + "/Scripts"));
app.use('/SoundEffects', express.static(__dirname + "/SoundEffects"));
//
//Routes
app.get('/', function (req, res) {
    res.sendFile(__dirname + '/Layouts/Layout.html');
});

app.post('/add', function (req, res) {
    addEdit.add(req, recipes, function (response) {
        res.send(response);
        res.end();
    })
    
});

app.get('/getRecipe', function (req, res) {
    addEdit.getRecipe(req, recipes, function (response) {
        res.send(response);
        res.end();
    })
});

app.post('/setEdit', function (req, res) {
    addEdit.edit(req, recipes, function (response) {
        res.send(response);
        res.end();
    })
});

app.get('/getAllRecipes', function (req, res) {
    search.recipes(req, recipes, function (response) {
        res.send(response);
        res.end();
    })
});

app.get('/search', function (req, res) {
    search.getRecipe(req, recipes, function (response) {
        res.send(response);
        res.end();
    })
})

app.post("/delete", function (req, res) {
    deleteMod.deleteRecipe(req, recipes, function (response) {
        res.send(response);
        res.end();
    });
});
//
http.createServer(app).listen(5555);