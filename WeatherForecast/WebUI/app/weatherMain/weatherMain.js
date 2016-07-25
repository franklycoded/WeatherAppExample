var weatherApp = angular.module('WeatherApp');
weatherApp.controller('WeatherMainController', ['$scope', function ($scope) {
    $scope.cityName = "";
    $scope.getForecast = function () {
        console.log("get forecast for " + $scope.cityName);
    }
}]);
