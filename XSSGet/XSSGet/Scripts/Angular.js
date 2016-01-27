var app = angular.module("app", []);

app.controller("xssGet", ["$scope", "$http", function ($scope, $http) {

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
            data: {"num": $scope.inputtedNumbers},
            success:  $scope.success,
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

    $scope.success = function (data){
        $scope.returnedNumbers = data;
    }

    $scope.error = function(data){
        console.log(data);
    }
}]);