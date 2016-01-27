var app = angular.module('app');

app.factory("SetTimeArraysService", function () {
    
    function setArrays() {
        
        this.set = function (hours, minutes, seconds) {
            
            for (var x = 0; x <= 23; x++) {
                hours.push(x);
            }
            
            for (var x = 0; x <= 59; x++) {
                minutes.push(x);
                seconds.push(x);
            }
        };
    }    ;
    
    return new setArrays();
});

app.factory("SharedPropertiesService", function () {
    //Timer Stuff
    function setValues() {
        
        var TimerModel = {
            Hours: 0,
            Minutes: 0,
            Seconds: 0
        }
        var TimerButtons = {
            Reset: true,
            Set: false,
            Pause: true
        }
        var pausedTimer = false;
        var timerValue = {};
        var activeTimer;
        
        this.setTimerModel = function (model) {
            TimerModel.Hours = model.Hours;
            TimerModel.Minutes = model.Minutes;
            TimerModel.Seconds = model.Seconds;
        },

        this.getTimerModel = function () {
            return TimerModel;
        },

        this.setTimerButtons = function (buttonsVal) {
            TimerButtons.Reset = buttonsVal.Reset;
            TimerButtons.Set = buttonsVal.Set;
            TimerButtons.Pause = buttonsVal.Pause;
        },

        this.getTimerButtons = function () {
            return TimerButtons;
        },

        this.setPausedTimerButton = function (button) {
            pausedTimer = button;
        },

        this.getPausedTimerButton = function () {
            return pausedTimer;
        },

        this.setTimerValue = function (model) {
            timerValue = model;
        },
        this.getTimerValue = function () {
            return timerValue;
        },
        this.setActiveTimer = function (timer) {
            activeTimer = timer;
        },
        this.getActiveTimer = function () {
            return activeTimer;
        }
    }    ;
    
    return new setValues();

});

app.factory("SearchPropertyStore", function () {
    function setValues() {
        
        var storedRecipe;
        var recipeName;
        
        this.setRecipe = function (recipe) {
            storedRecipe = recipe
        }
        
        this.getRecipe = function () {
            return storedRecipe;
        }
        
        this.setRecipeName = function (name) {
            recipeName = name;
        }
        this.getRecipeName = function () {
            return recipeName;
        }
    }
    return new setValues();
});

app.service("DeletePropertyStore", function () {
    
    var response;
    
    this.setDeleteRes = function (res) {
        response = res;
    },
    this.getDelteRes = function () {
        return response;
    }
});

app.service("HttpService", function ($http) {
    
    this.addRecipe = function (addForm, callback) {
        
        $http({
            method: "POST", url: "/add", data: addForm
        }).success(function (data) {
            
            $(".addForm").val('');
            addForm.Ingredients = [];
            addForm.Instructions = [];
            callback(data);
        }).error(function (config, headers, status, data) {
            
            console.log(config, headers, status, data);
        })
    },

    this.getRecipeForEdit = function (editRecipe, callback) {
        
        $http({
            method: "GET", url: "/getRecipe", params: { Name: editRecipe.Name }
        }).success(function (data) {
            
            callback(data);
        }).error(function (status, headers, config, data) {
            console.log(status, headers, config, data);
        })
    },

    this.setRecipeChanges = function (editRecipe, callback) {
        
        $http({
            method: 'POST', url: '/setEdit', data: editRecipe
        }).success(function (data) {
            callback(data);
            console.log(data);
        }).error(function (config, status, headers, data) {
            
            console.log(config, status, headers, data);
        })
    },

    this.getAllRecipes = function (callBack) {
        
        $http({ method: "GET", url: "/getAllRecipes" }).success(function (data) {
            
            callBack(data);
        }).error(function (data, status, config, headers) {
            console.log(data, status, config, headers);
        });
    },

    this.deleteRecipe = function (deleteRecipeName, callback) {
        
        if (!window.confirm("Are you sure you would like to delete this recipe? Once deleted it cannot be brought back")) {
             
        }
        else {
            $http({
                method: "Post", url: "/delete", data: { Name: deleteRecipeName.Name }
            }).success(function (data) {
                console.log(data);
                callback(data);
            
            }).error(function (config, headers, data, status) {
                console.log(config, headers, data, status);
            })
        }
    },

    this.search = function (q, callback) {
        $http({
            method: "GET", url: "/search", params: { query: q }
        }).success(function (data) {
                callback(data);
            }).error(function (data, status, headers, config) { 
                console.log(data, status, headers, config);
            })
    
    };

});

app.service("FormManipService", function () {
    
    this.addIngredient = function (form, input, callback) {
        form.Ingredients.push(input.Ingredient);
        callback();
    },

    this.deleteIngredient = function (form, ingredient) {
        var index = form.Ingredients.indexOf(ingredient);
        form.Ingredients.splice(index, 1);
    },

    this.addInstruction = function (form, input,callback) {
        form.Instructions.push(input.Instruction);
        callback();
    },

    this.deleteInstruction = function (form, instruction) {
        var index = form.Instructions.indexOf(instruction);
        form.Instructions.splice(index, 1);
    },

    this.ResetAddForm = function (addForm, callback) {
        $(".addForm").val('');
        callback();
    },

    this.hideEditMode = function (notInEditMode) {
        if (notInEditMode == false) {
            notInEditMode = true;
        }
    };
});

app.provider("TimerServiceProvider", function () {
    
    this.$get = function () {
        
        return {
            
            setTimer : function (times, display,callback) {
                
                var d = new Date();
                var fYear = d.getFullYear();
                var fMonth = d.getMonth();
                var fDay = d.getDate();
                var fHour = d.getHours() + times.Hours;
                var fMinute = d.getMinutes() + times.Minutes;
                var fSecond = d.getSeconds() + times.Seconds;
                var future = new Date(fYear, fMonth, fDay, fHour, fMinute, fSecond).getTime();
                
                var days = 0;
                var hours = 0;
                var minutes = 0;
                var seconds = 0;
                var returnTime = {};
                
                var timer = function () {
                    
                    var current = new Date().getTime();
                    var secondsLeft = (future - current) / 1000;
                    
                    if (secondsLeft <= 0) {
                        callback(secondsLeft);
                    }
                    
                    else {
                        
                        days = parseInt(secondsLeft / 86400);
                        secondsLeft = secondsLeft % 86400;
                        
                        hours = parseInt(secondsLeft / 3600);
                        secondsLeft = secondsLeft % 3600;
                        
                        minutes = parseInt(secondsLeft / 60);
                        seconds = parseInt(secondsLeft % 60);
                        
                        if (seconds > 0 || minutes > 0 || hours > 0) {
                            returnTime = { Hours: hours, Minutes: minutes, Seconds: seconds };
                            callback(returnTime);
                        }
                    }
                }
                
                callback(timer);
                
            }
        }
    }
});