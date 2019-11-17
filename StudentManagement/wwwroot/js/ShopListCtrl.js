'use strict'

var app = angular.module("app")
app.controller("ShopListCtrl", function ($scope, $http, $route) {

    $scope.$route = $route;

    function isEmptyOrSpaces(str){

        return str == null || str.match(/^\s*$/) != null;
    }

    $scope.getAllData = function (query = "") {

        var url = "api/v1/shops"
        if (!isEmptyOrSpaces(query))
        {
            url += "?query=" + query
        }

        $http({
            method: "GET",
            url: url
        }).then(function mySuccess(response) {

            $scope.shopList = response.data;

            $scope.editingData = {};

            for (var i = 0, length = $scope.shopList.length; i < length; i++) {
              $scope.editingData[$scope.shopList[i].id] = false;
            }

            for (var i = 0; i < $scope.shopList.length; ++i) 
            {
                var x = $scope.shopList[i]
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

            console.log(response.statusText);
        });
    }

    $scope.getAllData();

    $scope.add = function () {

        $scope.shop.status = parseInt($scope.shop.status, 10);
        console.log($scope.shop);
        $http.post("api/v1/shops", $scope.shop).then(function mySuccess(response) {

            console.log(response);

            $scope.getAllData("");
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

    $scope.modify = function(shopdata){
        $scope.editingData[shopdata.id] = true;
    };

    $scope.update = function(shopdata){
        $scope.editingData[shopdata.id] = false;
        console.log(shopdata);

        if(shopdata.status == "draft"){
            shopdata.status = 0;
        }
        else if(shopdata.status == "active"){
            shopdata.status = 1;
        }

        $http.put("api/v1/shops/" + shopdata.id, shopdata).then(function mySuccess(response) {

            console.log(response);

            $scope.getAllData();
        }, function myError(response) {

            console.log(response.statusText);
        });
    };

    $scope.search = function(querydata){
        $scope.getAllData(querydata)
    };


});