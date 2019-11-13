'use strict'

var app = angular.module("app")
app.controller("MainCtrl", function ($scope, $http) {

    $scope.getAllData = function () {

        $http({
            method: "GET",
            url: "api/v1/shops"
        }).then(function mySuccess(response) {

            $scope.shopList = response.data;
            console.log(response.data);

        }, function myError(response) {

            console.log(response.statusText);
        });
    }

    $scope.getAllData();

    $scope.add = function () {

        console.log($scope.shop);
        $http.post("api/v1/shops", $scope.shop).then(function mySuccess(response) {

            console.log(response);

            $scope.getAllData();
        }, function myError(response) {

            console.log(response.statusText);
        });

        $scope.shop = {};
    }

    $scope.del = function (id) {

        var url = "api/v1/shops/" + id;
        console.log(url);
        $http.delete(url).then(function mySuccess(response) {

            console.log(response);

            $scope.getAllData();
        }, function myError(response) {

            console.log(response.statusText);
        });

    }

    $scope.update = function () {

        console.log($scope.shop);
        $http.put("api/v1/shops", $scope.shop).then(function mySuccess(response) {

            console.log(response);

            $scope.getAllData();
        }, function myError(response) {

            console.log(response.statusText);
        });

    }
});