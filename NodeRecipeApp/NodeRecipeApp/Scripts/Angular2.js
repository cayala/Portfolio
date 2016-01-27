//var app = angular.module("app");

//app.config(["$routeProvider", function ($routeProvider) {
        
//        $routeProvider
//        .when('/add', { templateUrl: '/Views/Add.html' })
//        .when('/delete', { templateUrl: '/Views/Delete.html' })
//        .when('/edit', { templateUrl: '/Views/Edit.html' })
//        .when('/search', { templateUrl: '/Views/Search.html' })
//        .when('/recipe', { templateUrl: '/Views/Recipe.html' })
//        .when('/timer', { templateUrl: '/Views/Timer.html' })
//        .when('/home', { templateUrl: '/Views/Index.html' })
//        .when('/', { templateUrl: '/Views/Index.html' })
//        .otherwise({ redirectTo: '/Views/Index.html' });
//    }]);

//app.controller("Main", ['$http', '$scope', 'SetTimeArraysService', 'HttpService', 'FormManipService', "TimerServiceProvider", function ($http, $scope, SetTimeArraysService, HttpService, FormManipService, TimerServiceProvider) {
        
//        $scope.addForm = {
            
//            Id: -1,
//            Name: '',
//            Difficulty: '',
//            TypeOfDish: '',
//            Ingredients: [],
//            Instructions: [],
//            Image: ''
//        }
//        $scope.editRecipe = {
            
//            Id: -1,
//            Name: '',
//            Difficulty: '',
//            TypeOfDish: '',
//            Hours: 0,
//            Minutes: 0,
//            Ingredients: [],
//            Instructions: [],
//            Image: ''
//        }
//        $scope.displayRecipe = {
            
//            Id: 0,
//            Name: '',
//            Difficulty: '',
//            TypeOfDish: '',
//            Hours: 0,
//            Minutes: 0,
//            Ingredients: [],
//            Instructions: [],
//            Image: ''
//        }
//        $scope.TimerModel = {
            
//            Hours: 0,
//            Minutes: 0,
//            Seconds: 0
//        }
//        $scope.TimerArrays = {
            
//            Hours: [],
//            Minutes: [],
//            Seconds: []
//        }
//        $scope.addInputIngreIndtrucValid = {
//            Ingredient: '',
//            Instruction: ''
//        }
//        $scope.searchText = '';
//        $scope.listOfRecipes = [];
//        $scope.deleteRecipeName = { Name: '' }
//        $scope.notInEditMode = true;
//        $scope.loadingGif = true;
//        $scope.EditMode = false;
//        $scope.recipeExist = true;
//        $scope.recipeDeleteExist = true;
//        $scope.pausedTimer = false;
//        $scope.disableReset = true;
//        $scope.disableSet = false;
//        $scope.disablePause = true;
//        $scope.activeTimer;
//        $scope.navTimer = false;
        
//        $scope.setEdit = function (name) {
//            $scope.editRecipe.Name = name;
//        }
        
//        $scope.setDelete = function (name) {
//            $scope.deleteRecipeName.Name = name;
//        }
        
//        $scope.clearEditForm = function () {
            
//            $scope.EditMode = false;
//            $scope.recipeExist = true;
//            $scope.notInEditMode = true;
//            $scope.editRecipe = {
                
//                Id: -1,
//                Name: '',
//                Difficulty: '',
//                TypeOfDish: '',
//                Hours: 0,
//                Minutes: 0,
//                Ingredients: [],
//                Instructions: [],
//                Image: ''
//            }
//        }
        
//        $scope.addIngredient = function () {
            
//            if ($scope.addInputIngreIndtrucValid.Ingredient.length > 0 || $scope.addInputIngreIndtrucValid.Instruction.length > 0) {
                
//                if ($scope.editRecipe.Id >= 0) {
                    
//                    FormManipService.addIngredient($scope.editRecipe, function () {
//                        $scope.addInputIngreIndtrucValid = {
//                            Ingredient: '',
//                            Instruction: ''
//                        };
//                    });
//                } 
        
//                else {
                    
//                    FormManipService.addIngredient($scope.addForm, function () {
//                        $scope.addInputIngreIndtrucValid = {
//                            Ingredient: '',
//                            Instruction: ''
//                        };
//                    });
//                }
//            }
//        };
//        $scope.deleteIngredient = function (ingredient) {
            
//            if ($scope.editRecipe.Id >= 0) {
                
//                FormManipService.deleteIngredient($scope.editRecipe, ingredient);
//            } 
        
//            else {
                
//                FormManipService.deleteIngredient($scope.addForm, ingredient);
//            }
//        };
//        $scope.addInstruction = function () {
            
//            if ($scope.addInputIngreIndtrucValid.Instruction.length > 0) {
                
//                if ($scope.editRecipe.Id >= 0) {
                    
//                    FormManipService.addInstruction($scope.editRecipe, function () {
//                        $scope.addInputIngreIndtrucValid = {
//                            Ingredient: '',
//                            Instruction: ''
//                        };
//                    });
//                } 
        
//                else {
                    
//                    FormManipService.addInstruction($scope.addForm, function () {
//                        $scope.addInputIngreIndtrucValid = {
//                            Ingredient: '',
//                            Instruction: ''
//                        };
//                    });
//                }
//            }
//        };
//        $scope.deleteInstruction = function (instruction) {
//            if ($scope.editRecipe.Id >= 0) {
                
//                FormManipService.deleteInstruction($scope.editRecipe, instruction);
//            } 
        
//            else {
                
//                FormManipService.deleteInstruction($scope.addForm, instruction);
//            }
//        };
//        $scope.ResetAddForm = function () {
            
//            FormManipService.ResetAddForm($scope.addForm, function () {
//                $scope.addForm = {
                    
//                    Id: 0,
//                    Name: '',
//                    Difficulty: '',
//                    TypeOfDish: '',
//                    Ingredients: [],
//                    Instructions: [],
//                    Image: ''
//                };
//            });
//        }
//        $scope.hideEditMode = function () {
            
//            FormManipService.hideEditMode($scope.notInEditMode);
//        };
        
//        $scope.setRecipeForDisplay = function (recipe) {
            
//            $scope.displayRecipe = recipe;
//        };
        
//        SetTimeArraysService.set($scope.TimerArrays.Hours, $scope.TimerArrays.Minutes, $scope.TimerArrays.Seconds);
        
//        $scope.addRecipe = function () {
            
//            $scope.loadingGif = false;
            
//            HttpService.addRecipe($scope.addForm, function (returnedData) {
                
//                $scope.loadingGif = true;
//            });
//        }
        
//        $scope.getRecipeForEdit = function () {
//            HttpService.getRecipeForEdit($scope.editRecipe, function (returnedData) {
                
//                if (!returnedData) {
//                    $scope.recipeExist = false;
//                }
//                else {
//                    $scope.EditMode = true;
//                    $scope.notInEditMode = false;
//                    $scope.editRecipe = returnedData;
//                }
//            });
//        };
        
//        $scope.setRecipeChanges = function () {
//            HttpService.setRecipeChanges($scope.editRecipe);
//        }
        
//        $scope.getAllRecipes = function () {
            
//            HttpService.getAllRecipes(function (returnedData) {
//                $scope.listOfRecipes = returnedData;
//            });
//        };
        
//        $scope.deleteRecipe = function () {
            
//            HttpService.deleteRecipe($scope.deleteRecipeName, function (returnedData) {
                
//                $scope.recipeDeleteExist = returnedData;
//            });
//        }
        
//        $scope.pausedTimerValue = {};
        
//        $scope.setTimer = function () {
            
//            $scope.disableSet = true;
//            $scope.disableReset = false;
//            $scope.disablePause = false
//            TimerServiceProvider.setTimer($scope.TimerModel, function (returnedTimer) {
//                if (returnedTimer.Seconds > 0 || returnedTimer.Minutes > 0 || returnedTimer.Hours > 0) {
//                    $scope.pausedTimerValue = returnedTimer;
//                }
//                else if (returnedTimer <= 0) {
//                    clearInterval($scope.activeTimer);
//                    document.getElementById("soundEffect").play();
//                    alert("Timer is done!");
//                }
//                else {
//                    $scope.activeTimer = setInterval(returnedTimer, 1000);
//                    $scope.pausedTimer = false;
//                    $scope.navTimer = false;
//                }
//            });
//        }
        
//        $scope.pauseTimer = function () {
            
//            $scope.disableSet = true;
//            $scope.disableReset = false;
//            if (!$scope.pausedTimer) {
                
//                clearInterval($scope.activeTimer);
//                $scope.pausedTimer = true;
//                $scope.disableSet = false;
//            }
//            else {
//                TimerServiceProvider.setTimer($scope.pausedTimerValue, function (returnedTimer) {
                    
//                    if (returnedTimer.Seconds > 0 || returnedTimer.Minutes > 0 || returnedTimer.Hours > 0) {
//                        $scope.pausedTimerValue = returnedTimer;
//                    }
//                    else if (returnedTimer <= 0) {
//                        clearInterval($scope.activeTimer);
//                        $scope.diableSet = false;
//                        $scope.disableReset = true;
//                        $scope.pausedTimer = false;
//                        document.getElementById("soundEffect").play();
//                        alert("Timer is done!");
//                    }

//                    else {
//                        $scope.activeTimer = setInterval(returnedTimer, 1000);
//                        $scope.pausedTimer = false;
//                    }
//                });
//            }
            
//        }
        
//        $scope.resetTimer = function () {
            
//            clearInterval($scope.activeTimer);
            
//            TimerServiceProvider.setTimer($scope.TimerModel, function (returnedTimer) {
                
//                if (returnedTimer.Seconds > 0 || returnedTimer.Minutes > 0 || returnedTimer.Hours > 0) {
//                    $scope.pausedTimerValue = returnedTimer;
//                }

//                else if (returnedTimer <= 0) {
                    
//                    clearInterval($scope.activeTimer);
//                    $scope.disableSet = false;
//                    $scope.disableReset = true;
//                    document.getElementById("soundEffect").play();
//                    alert("Timer is done!");
//                }

//                else {
//                    $scope.activeTimer = setInterval(returnedTimer, 1000);
//                    $scope.disableSet = true;
//                    $scope.disableReset = false;
//                    $scope.navTimer = false;
//                }

//            });
//        }
        
//        $scope.hideNavTimer = function () {
            
//            $scope.navTimer = true;
//        }



//    }]);