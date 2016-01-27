var app = angular.module("app", ["ngRoute"]);

app.config(["$routeProvider", function ($routeProvider) {
        
        $routeProvider
        .when('/add', { templateUrl: '/Views/Add.html', controller: "addEditForms" })
        .when('/delete', { templateUrl: '/Views/Delete.html', controller: "deleteForms" })
        .when('/edit', { templateUrl: '/Views/Edit.html', controller: "addEditForms" })
        .when('/search', { templateUrl: '/Views/Search.html', controller: "search" })
        .when('/recipe', { templateUrl: '/Views/Recipe.html', controller: "search" })
        .when('/timer', { templateUrl: '/Views/Timer.html', controller: "timer" })
        .when('/home', { templateUrl: '/Views/Index.html' })
        .when('/', { templateUrl: '/Views/Index.html' })
        .otherwise({ redirectTo: '/Views/Index.html' });
    }]);

app.controller("layout", ["$scope", 'SharedPropertiesService', 'TimerServiceProvider', 'HttpService', "$http", function ($scope, SharedPropertiesService, TimerServiceProvider, HttpService, $http) {
        //Timer
        $scope.layoutTimer = {
            Hours: 0,
            Minutes: 0,
            Seconds: 0
        }
        $scope.layoutDisableButtons = SharedPropertiesService.getTimerButtons();
        $scope.layoutPausedTimerValue = {};
        $scope.layoutPausedTimer = false;
        $scope.layoutActiveTimer;
        
        $scope.pauseTimer = function () {
            
            $scope.layoutPausedTimer = SharedPropertiesService.getPausedTimerButton();
            
            if (!$scope.layoutPausedTimer) {
                $scope.layoutActiveTimer = SharedPropertiesService.getActiveTimer();
                clearInterval($scope.layoutActiveTimer);
                $scope.layoutPausedTimer = true;
                SharedPropertiesService.setPausedTimerButton($scope.layoutPausedTimer);
                $scope.layoutDisableButtons.Set = false;
                SharedPropertiesService.setTimerButtons($scope.layoutDisableButtons);
            }
            else {
                $scope.layoutPausedTimerValue = SharedPropertiesService.getTimerValue();
                
                TimerServiceProvider.setTimer($scope.layoutPausedTimerValue, function (returnedTimer) {
                    
                    if (returnedTimer.Seconds > 0 || returnedTimer.Minutes > 0 || returnedTimer.Hours > 0) {
                        $scope.layoutPausedTimerValue = returnedTimer;
                        SharedPropertiesService.setTimerValue($scope.layoutPausedTimerValue);
                    }
                    else if (returnedTimer <= 0) {
                        clearInterval($scope.layoutActiveTimer);
                        $scope.layoutDisableButtons.Set = false;
                        $scope.layoutDisableButtons.Reset = true;
                        SharedPropertiesService.setTimerButtons($scope.layoutDisableButtons);
                        $scope.layoutPausedTimer = false;
                        SharedPropertiesService.setPausedTimerButton($scope.layoutPausedTimer);
                        document.getElementById("soundEffect").play();
                        alert("Timer is done!");
                    }

                    else {
                        $scope.layoutActiveTimer = setInterval(returnedTimer, 1000);
                        SharedPropertiesService.setActiveTimer($scope.layoutActiveTimer);
                        $scope.layoutPausedTimer = false;
                        SharedPropertiesService.setPausedTimerButton($scope.layoutPausedTimer);
                    }
                });
            }
        }
        
        $scope.resetTimer = function () {
            
            $scope.layoutActiveTimer = SharedPropertiesService.getActiveTimer();
            clearInterval($scope.layoutActiveTimer);
            $scope.layoutTimer = SharedPropertiesService.getTimerModel();
            
            TimerServiceProvider.setTimer($scope.layoutTimer, function (returnedTimer) {
                
                if (returnedTimer.Seconds > 0 || returnedTimer.Minutes > 0 || returnedTimer.Hours > 0) {
                    $scope.layoutPausedTimerValue = returnedTimer;
                    SharedPropertiesService.setTimerValue($scope.layoutPausedTimerValue);
                }

                else if (returnedTimer <= 0) {
                    
                    clearInterval($scope.layoutActiveTimer);
                    $scope.layoutDisableButtons.Set = false;
                    $scope.layoutDisableButtons.Reset = true;
                    $scope.layoutPausedTimer = false;
                    SharedPropertiesService.setPausedTimerButton($scope.layoutPausedTimer);
                    document.getElementById("soundEffect").play();
                    alert("Timer is done!");
                }

                else {
                    $scope.layoutActiveTimer = setInterval(returnedTimer, 1000);
                    SharedPropertiesService.setActiveTimer($scope.layoutActiveTimer);
                    $scope.layoutDisableButtons.Set = true;
                    $scope.layoutDisableButtons.Reset = false;
                    SharedPropertiesService.setTimerButtons($scope.layoutDisableButtons);
                    $scope.layoutPausedTimer = false;
                    SharedPropertiesService.setPausedTimerButton($scope.layoutPausedTimer);
                }

            });
        }
        
        $scope.getNum = function (){
            $http({
                method:"get", url: "http://localhost:58783/XSSGiver/GiveArray", params: { num: 5 }
            }).success(function (data){
                console.log(data);
            }).error(function (config, status, headers){
                console.log(config, status, headers);
            })
        }

        //
        //SearchBar
        $scope.fireGetAllRecipes = function () {
            
            $rootScope.on('')
        };

        //
    }]);

app.controller("addEditForms", ["$scope", "FormManipService", "HttpService", 'SearchPropertyStore', function ($scope, FormManipService, HttpService, SearchPropertyStore) {
        $scope.addForm = {
            _id: 0,
            Id: -1,
            Name: '',
            Difficulty: '',
            TypeOfDish: '',
            Ingredients: [],
            Instructions: [],
            Image: ''
        }
        $scope.editRecipe = {
            _id: 0,
            Id: -1,
            Name: SearchPropertyStore.getRecipeName(),
            Difficulty: '',
            TypeOfDish: '',
            Hours: 0,
            Minutes: 0,
            Ingredients: [],
            Instructions: [],
            Image: ''
        }
        $scope.addInputIngreIndtrucValid = {
            Ingredient: '',
            Instruction: ''
        }
        $scope.loadingGif = true;
        $scope.EditMode = false;
        $scope.recipeExist = true;
        $scope.notInEditMode = true;
        $scope.formSubmitSuccess = false;
        $scope.formSubmitError = true;
        
        $scope.addIngredient = function () {
            
            if ($scope.addInputIngreIndtrucValid.Ingredient.length > 0 || $scope.addInputIngreIndtrucValid.Instruction.length > 0) {
                
                if ($scope.editRecipe.Id >= 0) {
                    
                    FormManipService.addIngredient($scope.editRecipe, $scope.addInputIngreIndtrucValid, function () {
                        $scope.addInputIngreIndtrucValid.Ingredient = '';
                    });
                } 

                else {
                    
                    FormManipService.addIngredient($scope.addForm, $scope.addInputIngreIndtrucValid, function () {
                        $scope.addInputIngreIndtrucValid.Ingredient = '';
                    });
                }
            }
        };
        $scope.deleteIngredient = function (ingredient) {
            
            if ($scope.editRecipe.Id >= 0) {
                
                FormManipService.deleteIngredient($scope.editRecipe, ingredient);
            } 

            else {
                
                FormManipService.deleteIngredient($scope.addForm, ingredient);
            }
        };
        $scope.addInstruction = function () {
            
            if ($scope.addInputIngreIndtrucValid.Instruction.length > 0) {
                
                if ($scope.editRecipe.Id >= 0) {
                    
                    FormManipService.addInstruction($scope.editRecipe, $scope.addInputIngreIndtrucValid,function () {
                        $scope.addInputIngreIndtrucValid.Instruction = '';
                    });
                } 

                else {
                    
                    FormManipService.addInstruction($scope.addForm, $scope.addInputIngreIndtrucValid,function () {
                        $scope.addInputIngreIndtrucValid.Instruction = '';
                    });
                }
            }
        };
        $scope.deleteInstruction = function (instruction) {
            if ($scope.editRecipe.Id >= 0) {
                
                FormManipService.deleteInstruction($scope.editRecipe, instruction);
            } 

            else {
                
                FormManipService.deleteInstruction($scope.addForm, instruction);
            }
        };
        $scope.ResetAddForm = function () {
            
            FormManipService.ResetAddForm($scope.addForm, function () {
                $scope.AddForm.$setPristine();
                $scope.AddForm.$setUntouched();
                $scope.addForm = {
                    Ingredients: [],
                    Instructions: []
                };
                $scope.formSubmitSuccess = false;
                $scope.formSubmitError = true;
            });
        };
        $scope.getRecipeForEdit = function () {
            HttpService.getRecipeForEdit($scope.editRecipe, function (returnedData) {
                
                if (!returnedData) {
                    $scope.recipeExist = false;
                    $scope.formSubmitSuccess = false;
                }
                else {
                    $scope.EditMode = true;
                    $scope.notInEditMode = false;
                    $scope.editRecipe = returnedData;
                    $scope.editForm.$setUntouched();
                }
            });
        };
        $scope.setRecipeChanges = function () {
            HttpService.setRecipeChanges($scope.editRecipe, function (returnedData) {
                $scope.formSubmitSuccess = returnedData;
                $scope.EditMode = false;
                $scope.notInEditMode = true;
                $scope.recipeExist = true;
            });
        };
        $scope.addRecipe = function () {
            
            $scope.loadingGif = false;
            
            HttpService.addRecipe($scope.addForm, function (returnedData) {
                
                $scope.formSubmitSuccess = returnedData;
                $scope.formSubmitError = returnedData;
                $scope.loadingGif = true;
                $scope.addForm = {
                    Ingredients: [],
                    Instructions: []
                };
                $scope.AddForm.$setPristine();
                $scope.AddForm.$setUntouched();
            });

        };
    }]);

app.controller("deleteForms", ["$scope", "HttpService", 'SearchPropertyStore', 'DeletePropertyStore', function ($scope, HttpService, SearchPropertyStore, DeletePropertyStore) {
        $scope.deleteRecipeName = { Name: SearchPropertyStore.getRecipeName() };
        $scope.recipeDeleteSuccess = false;
        $scope.recipeDeleteError = true;
        
        $scope.deleteRecipe = function () {
            
            HttpService.deleteRecipe($scope.deleteRecipeName, function (returnedData) {
                DeletePropertyStore.setDeleteRes(returnedData);
                $scope.recipeDeleteSuccess = returnedData;
                $scope.recipeDeleteError = returnedData;
            });
        }
    }]);

app.controller("search", ["$scope", "HttpService", "SearchPropertyStore", function ($scope, HttpService, SearchPropertyStore) {
        $scope.searchText = '';
        $scope.displayRecipe = SearchPropertyStore.getRecipe();
        $scope.listOfRecipes = [];
        
        HttpService.getAllRecipes(function (returnedData) {
            $scope.listOfRecipes = returnedData;
        });
        
        $scope.setRecipeForDisplay = function (recipe) {
            
            SearchPropertyStore.setRecipe(recipe);
        };
        $scope.setName = function (name) {
            SearchPropertyStore.setRecipeName(name);
        };

        $scope.search = function () {
            HttpService.search($scope.searchText, function (returnedData) {
                $scope.listOfRecipes = [];
                $scope.listOfRecipes = returnedData;
            })
        };

        $scope.refresh = function () {
            HttpService.getAllRecipes(function (returnedData) {
                $scope.listOfRecipes = [];
                $scope.listOfRecipes = returnedData;
            });
        }


    }]);

app.controller("timer", ['$scope', 'TimerServiceProvider', 'SetTimeArraysService', 'SharedPropertiesService', function ($scope, TimerServiceProvider, SetTimeArraysService, SharedPropertiesService) {
        $scope.TimerModel = {
            Hours: 0,
            Minutes: 0,
            Seconds: 0
        }
        $scope.TimerArrays = {
            Hours: [],
            Minutes: [],
            Seconds: []
        }
        $scope.disableButtons = {
            Reset: true,
            Set: false,
            Pause: true
        }
        $scope.timerDisplay;

        $scope.pausedTimer = false;
        $scope.activeTimer;
        $scope.pausedTimerValue = {};
        
        SetTimeArraysService.set($scope.TimerArrays.Hours, $scope.TimerArrays.Minutes, $scope.TimerArrays.Seconds);
        
        $scope.setTimer = function () {
            
            $scope.activeTimer = SharedPropertiesService.getActiveTimer();
            
            clearInterval($scope.activeTimer);
            
            $scope.disableButtons.Set = true;
            $scope.disableButtons.Reset = false;
            $scope.disableButtons.Pause = false;
            SharedPropertiesService.setTimerButtons($scope.disableButtons);
            TimerServiceProvider.setTimer($scope.TimerModel, $scope.timerDisplay,function (returnedTimer) {
                if (returnedTimer.Seconds > 0 || returnedTimer.Minutes > 0 || returnedTimer.Hours > 0) {
                    $scope.pausedTimerValue = returnedTimer;
                    SharedPropertiesService.setTimerValue($scope.pausedTimerValue);
                    $scope.timerDisplay = returnedTimer;
                }
                else if (returnedTimer <= 0) {
                    clearInterval($scope.activeTimer);
                    document.getElementById("soundEffect").play();
                    alert("Timer is done!");
                }
                else {
                    $scope.activeTimer = setInterval(returnedTimer, 1000);
                    SharedPropertiesService.setActiveTimer($scope.activeTimer);
                    $scope.pausedTimer = false;
                    SharedPropertiesService.setPausedTimerButton($scope.pausedTimer);
                    SharedPropertiesService.setTimerModel($scope.TimerModel);
                }
            });
        }
        
        $scope.pauseTimer = function () {
            
            $scope.disableButtons.Set = true;
            $scope.disableButtons.Reset = false;
            
            SharedPropertiesService.setTimerButtons($scope.disableButtons);
            
            $scope.pausedTimer = SharedPropertiesService.getPausedTimerButton();
            
            if (!$scope.pausedTimer) {
                $scope.activeTimer = SharedPropertiesService.getActiveTimer();
                clearInterval($scope.activeTimer);
                $scope.pausedTimer = true;
                SharedPropertiesService.setPausedTimerButton($scope.pausedTimer);
                $scope.disableButtons.Set = SharedPropertiesService.getTimerButtons();
                $scope.disableButtons.Set = false;
            }
            else {
                
                $scope.pausedTimerValue = SharedPropertiesService.getTimerValue();
                
                TimerServiceProvider.setTimer($scope.pausedTimerValue, $scope.timerDisplay,function (returnedTimer) {
                    
                    if (returnedTimer.Seconds > 0 || returnedTimer.Minutes > 0 || returnedTimer.Hours > 0) {
                        $scope.pausedTimerValue = returnedTimer;
                        SharedPropertiesService.setTimerValue($scope.pausedTimerValue);
                    }
                    else if (returnedTimer <= 0) {
                        clearInterval($scope.activeTimer);
                        $scope.disableButtons.Set = false;
                        $scope.disableButtons.Reset = true;
                        $scope.pausedTimer = false;
                        SharedPropertiesService.setPausedTimerButton($scope.pausedTimer);
                        document.getElementById("soundEffect").play();
                        alert("Timer is done!");
                    }

                    else {
                        $scope.activeTimer = setInterval(returnedTimer, 1000);
                        SharedPropertiesService.setActiveTimer($scope.activeTimer);
                        $scope.pausedTimer = false;
                        SharedPropertiesService.setPausedTimerButton($scope.pausedTimer);
                    }
                });
            }
            
        }
        
        $scope.resetTimer = function () {
            
            $scope.activeTimer = SharedPropertiesService.getActiveTimer();
            clearInterval($scope.activeTimer);
            $scope.TimerModel = SharedPropertiesService.getTimerModel();
            
            TimerServiceProvider.setTimer($scope.TimerModel, $scope.timerDisplay,function (returnedTimer) {
                
                if (returnedTimer.Seconds > 0 || returnedTimer.Minutes > 0 || returnedTimer.Hours > 0) {
                    $scope.pausedTimerValue = returnedTimer;
                    SharedPropertiesService.setTimerValue($scope.pausedTimerValue);
                }
                else if (returnedTimer <= 0) {
                    clearInterval($scope.activeTimer);
                    $scope.disableButtons.Set = false;
                    $scope.disableButtons.Reset = true;
                    document.getElementById("soundEffect").play();
                    alert("Timer is done!");
                }
                else {
                    $scope.activeTimer = setInterval(returnedTimer, 1000);
                    SharedPropertiesService.setActiveTimer($scope.activeTimer);
                    $scope.disableButtons.Set = true;
                    $scope.disableButtons.Reset = false;
                    SharedPropertiesService.setTimerButtons($scope.disableButtons);
                    $scope.pausedTimer = false;
                    SharedPropertiesService.setPausedTimerButton($scope.pausedTimer);
                }
            });
        }
       
    }]);

app.controller("crossDomain", ["$scope", "$http", function ($scope, $http) {
    $scope.inputtedNumbers = 0;
    $scope.returnedNumbers;
    
    //ajax
    $scope.ajaxGet = function () {
        $http({
            method: "GET", url: "http://localhost:58783/XSSGiver/ajaxGet", params: { num: $scope.inputtedNumbers }
        }).success(function (data) {
            
            $scope.returnedNumbers = data;

        }).error(function (config, headers, status) {
            console.log(config, headers, status);
        })
    }
    
    $scope.ajaxPost = function () {
        $http({
            method: "POST", url: "http://localhost:58783/XSSGiver/ajaxPost?num=" + $scope.inputtedNumbers
        }).success(function (data) {
            
            $scope.returnedNumbers = data;

        }).error(function (config, headers, status) {
            console.log(config, headers, status);
        })
    }
    
    //JSONP
    window.jsonpcallback = function (data) {
        window.results = data;
    }
    
    $scope.getViaJsonp = function () {
        
        var url = "http://localhost:58783/XSSGiver/GiveJSONPArray?num=" + $scope.inputtedNumbers + "&callback=jsonpcallback";
        
        jQuery.getScript(url, function () {
            $scope.returnedNumbers = window.results;
            console.log($scope.returnedNumbers);
        })
    }
    
    //nothing really, initial test with getting a webpage from another site
    $scope.getWebPage = function () {
        $http({
            method: "get", url: "http://localhost:58783/XSSGiver/Index"
        }).success(function (data) {
            console.log(data);
        }).error(function (config, status, headers) {
            console.log(config, status, headers);
        })
    }
    
    //cors ajax call
    $scope.getNumbers = function () {
        
        var url = "http://localhost:58783/XSSGiver/GiveArray";
        //$.get(url);
        var settings = {
            method: "GET",
            data: { "num": $scope.inputtedNumbers },
            success: $scope.success,
            error: $scope.error,
            crossDomain: true,
        }
        jQuery.ajax(url, settings);

        //$http({
        //    method: "GET", url: "http://localhost:58783/XSSGiver/GiveArray"
        //}).success(function (data) {
        //    console.log(data);
        //}).error(function (config, status, headers) {
        //    console.log(config, status, headers);
        //})
    }
    
    $scope.postNumbers = function () {
        var url = "http://localhost:58783/XSSGiver/PostNumberToArray?num=" + $scope.inputtedNumbers
        
        var settings = {
            method: "POST",
            data: $scope.inputtedNumbers,
            success: $scope.success,
            error: $scope.error,
            crossDomain: true,
        }
        jQuery.ajax(url, settings);
    }
    
    $scope.success = function (data) {
        $scope.returnedNumbers = data;
    }
    
    $scope.error = function (data) {
        console.log(data);
    }
}]);

