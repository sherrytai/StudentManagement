'use strict'

var app=angular.module("app")
app.controller("RegisterCtrl", function($scope, $http, $route){
		 
		$scope.$route = $route;

		$scope.register = function(){

            console.log($scope.user);

			$http.post("api/v1/accounts", $scope.user).then(function mySuccess(response) {

                // TODO redirect to other page
	            console.log(response);

	        }, function myError(response) {
              
	            console.log(response);
                alert(response.data.message);
	        });

			$scope.user = {};
		}

	});