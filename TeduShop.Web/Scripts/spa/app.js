/// <reference path="../plugins/angular/angular.js" />
var myApp = angular.module('mymodule', []);

myApp.controller("schoolController", schoolController);

myApp.directive("teduShopDirective", teduShopDirective);

myApp.service('validatorService', validatorService);

schoolController.$inject = ['$scope', 'validatorService'];


function schoolController($scope, validatorService) {
    $scope.checkNumber = function() {
        $scope.message = validatorService.checkNumber($scope.num);
    }

    $scope.num = 1;
}

function validatorService($window) {
    return {
        checkNumber: checkNumber
    }
    function checkNumber(input) {
        if (input % 2 == 0) {
            return 'This is even';
        }
        else {
            return 'This is old';
        }
    }
}

function teduShopDirective() {
    return {
        templateUrl: "/Scripts/spa/teduShopDirective.html"
    }
}