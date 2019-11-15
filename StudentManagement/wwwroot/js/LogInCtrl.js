'use strict'

var app=angular.module("app")
app.controller("SignInCtrl", function($scope, $http, $route){

	$scope.$route = $route;

	$scope.signIn = function(){

        console.log($scope.user);
		$http.post("api/v1/accounts/login", $scope.user).then(function mySuccess(response) {

            console.log(response);

        }, function myError(response) {

            console.log(response);
            alert(response.data.message);
        });
	}

});
