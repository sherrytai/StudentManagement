'use strict'

var app=angular.module("app")
app.controller("ShopCtrl", function($scope, $http, $route){
    $http({
	            method : "GET",
	            url : "api/v1/shops/1"
	        }).then(function mySuccess(response) {

	            $scope.shop = response.data;
	            console.log(response.data);

	        }, function myError(response) {

	            console.log(response.statusText);
	        });
});