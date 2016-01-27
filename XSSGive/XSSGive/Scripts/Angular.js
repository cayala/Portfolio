var app = angular.module("app", []);

app.controller("controller", ["$scope", "$http", function ($scope, $http) {

    $scope.domains;
    $scope.selectedDomain;
    $scope.newDomain;

    $scope.addDomain = function () {
        $http({
            method:"post", url:"/XSSGiver/addKey", data: {domain: $scope.newDomain}
        }).success(function (data) {
            console.log("did something");
            $scope.domains = data;
        }).error(function (status, config, headers) {
            console.log(status, config, headers);
        })
    }

    $scope.deleteDomain = function () {
        $http({
            method: "post", url: "/XSSGiver/deleteKey", data: { domain: $scope.selectedDomain }
        }).success(function (data) {
            console.log("success");
            $scope.domains = data;
        }).error(function (headers, status, config) {
            console.log(headers, status, config);
        })
    }

    $scope.getDomains = function () {
        $http({
            method: "GET", url: "/XSSGiver/getDomains"
        }).success(function (data){
            $scope.domains = data;
            console.log(data);
        }).error(function (config, status, headers) {
            console.log(config, status, headers);
        })
    }

    $scope.getDomains();
}]);