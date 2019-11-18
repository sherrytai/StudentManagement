'use strict'

var app=angular.module("app")
app.controller("HomeCtrl", function($scope, $http, $route){

		$scope.$route = $route;


		$http({
		            method : "GET",
		            url : "api/v1/products"
		        }).then(function mySuccess(response) {

		            $scope.products = response.data;
		            console.log(response.data);

		        }, function myError(response) {

	                    console.log(response);
	                    alert(response.data.message);

		        });

});