var app = angular.module("app", []);

app.controller("SearchCtrl", ["$scope", "SearchService", "$timeout",function ($scope, SearchService, $timeout) {

    $scope.SearchTerms = '';
    $scope.deleteStatusBar = true;

    $scope.Search = function () {
        SearchService.searchHistory($scope.SearchTerms, function (data) {
            if (data == null || undefined) {
                console.log("Data is null or undefined");
            }
            console.log(data);
            $scope.ResList = [];
            $scope.ResList = data;
        });
    }

    $scope.AddNewRes = function () {
        SearchService.saveRestaurant($scope.Restaurant, function (data) {
            console.log("Callback= " + data);
            $scope.ListAllRestaurants();
        });
    }

    $scope.ListAllRestaurants = function () {
        SearchService.getAllRestaurants(function (data) {
            if (data == null || undefined) {
                console.log("Data is null or undefined");
            }
            $scope.ResList = [];
            $scope.ResList = data;
        });
    }

    $scope.updateRestaurant = function ()
    {
        SearchService.updateRestaurant(updateForm, function (data) {
            console.log("success = " + data);
            $scope.updateStatusBar = false;
            $scope.updateResponse = data;
            if (data)
                $scope.ListAllRestaurants();
            else
            {
                $timeout(function () {
                    $scope.updateStatusBar = true;
                }, 4000);
            }

        });
    }

    $scope.DeleteRestaurant = function () {
        SearchService.removeRestaurant($scope.deleteTerm, function (data) {
            $scope.deleteStatusBar = false;
            $scope.deleteResponse = data;
            if (data) {
                $scope.ListAllRestaurants();
            }
            console.log(data);
            $timeout(function () {
                $scope.deleteStatusBar = true;
            }, 4000);
        });
    }

    $scope.ListAllRestaurants();
}]);

app.controller("logstash", ["$scope", "$http", function ($scope, $http) {
    
    $scope.getLogs = function ()
    {
        $http({
            method:"GET", url:"/LogStash/GetLogs"
        }).success(function (data)
        {
            console.log(data);
            $scope.iisLogs = data;
        }).error(function (config, headers, status, data)
        {
            console.log(config, headers, status, data);
        })
    }

    $scope.getLogs();
}]);