var app = angular.module("app");

app.service("SearchService", ["$http", function ($http) {

    this.searchHistory = function (searchTerms, callback) {
        $http({
            method: "GET", url: "/Search/GetHistory", params: { search: searchTerms }
        }).success(function (data) {
            callback(data);
        }).error(function (config, headers, status, data) {
            console.log(config, headers, status, data);
        })
    }

    this.saveRestaurant = function (resObject, callback) {
        $http({
            method: "POST", url: "/Search/SaveNewRestaurant", data: {r: resObject}
        }).success(function (data) {
            callback(data);
        }).error(function (config, headers, status, data) {
            console.log(config, headers, status, data);
        })
    }

    this.getAllRestaurants = function (callback) {
        $http({
            method: "GET", url: "/Search/GetAllResults"
        }).success(function (data) {
            callback(data);
        }).error(function (config, headers, status, data) {
            console.log(config, headers, status, data);
        })
    }

    this.removeRestaurant = function (resName, callback) {
        $http({
            method:"POST", url:"/Search/RemoveRestaurant", data: {rName: resName}
        }).success(function (data) {
            callback(data);
        }).error(function (config, headers, status, data) {
            console.log(config, headers, status, data);
        })
    }

    this.updateRestaurant = function (upRes, callback)
    {
        $http({
            method: "POST", url: "/Search/UpdateRestaurant", data: {rName: upRes.RName, lName: LName}
        })
            .success(function (data) {
                callback(data)
            })
            .error(function (status, config, headers, data) {
            console.log(status, config, headers, data);
        })
    }
}]);