'use strict'

var app=angular.module("app")
app.controller("ShopCtrl", function($scope, $http, $route){

    $scope.GetDataById = function(id)
    {
    	var url = "api/v1/shops/" + id 
    	$http({
	            method : "GET",
	            url : url
	        }).then(function mySuccess(response) {

	            $scope.shop = response.data;
	            console.log(response.data);

	        }, function myError(response) {

                    console.log(response);
                    alert(response.data.message);
	        });
	}

    $scope.getAllData = function () {
	    $http({
		            method : "GET",
		            url : "api/v1/shops/"+ $scope.selectedShop.id +"/products"
		        }).then(function mySuccess(response) {

		            $scope.products = response.data;

		            $scope.editingData = {};

		            for (var i = 0, length = $scope.products.length; i < length; i++) {
		              $scope.editingData[$scope.products[i].id] = false;
		            }

		            for (var i = 0; i < $scope.products.length; ++i) 
		            {
		                var x = $scope.products[i]
		                if(x.status == 0){
		                    x.status = "draft";
		                }
		                else if(x.status == 1){
		                    x.status = "active";
		                }
		                console.log(x);
		            }

		            console.log(response.data);

		        }, function myError(response) {


	                    console.log(response);
	                    alert(response.data.message);
		        });
    }

	$scope.add = function () {

        $scope.product.status = parseInt($scope.product.status, 10);
        $scope.product.shopId = $scope.selectedShop.id
        console.log($scope.product);
        $http.post("api/v1/products", $scope.product).then(function mySuccess(response) {

            console.log(response);

            $scope.getAllData();
        }, function myError(response) {

            console.log(response.statusText);
            alert(response.data.message);
        });

        $scope.product = {};
    }

    $scope.del = function (id) {

        var url = "api/v1/products/" + id;
        console.log(url);
        $http.delete(url).then(function mySuccess(response) {

            console.log(response);

            $scope.getAllData();
        }, function myError(response) {

            console.log(response.statusText);
            alert(response.data.message);
        });

    }

    $scope.modify = function(productdata){
        $scope.editingData[productdata.id] = true;
    };

    $scope.update = function(productdata){
        $scope.editingData[productdata.id] = false;
        

        if(productdata.status == "draft"){
            productdata.status = 0;
        }
        else if(productdata.status == "active"){
            productdata.status = 1;
        }

        console.log(productdata);

        $http.put("api/v1/products/" + productdata.id, productdata).then(function mySuccess(response) {

            console.log(response);

            $scope.getAllData();
        }, function myError(response) {

            console.log(response);
            alert(response.data.message);
        });
    };

    $scope.Initialize = function()
    {
    	$scope.$route = $route;

		$http({
	            method: "GET",
	            url: "api/v1/shops"
	        }).then(function mySuccess(response) {

	            $scope.shopList = response.data;

	            $scope.selectedShop = $scope.shopList[0]
	     		$scope.GetDataById($scope.selectedShop.id)
	     		$scope.getAllData()

	            console.log(response.data);

	        }, function myError(response) {

	            console.log(response.statusText);
	        });
    }

    $scope.Initialize()
});