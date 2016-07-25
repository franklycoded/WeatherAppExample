var weatherApp = angular.module('WeatherApp');

weatherApp.controller('WeatherMainController', ['$scope', 'forecastService', function ($scope, forecastService) {
    $scope.cityName = "";
    $scope.weatherData = {};
    $scope.getForecast = function () {
        console.log("get forecast for " + $scope.cityName);

        forecastService.getForecast($scope.cityName)
            .then(function (weatherData) {
                console.log(weatherData);
                $scope.weatherData = weatherData.data;
            });
    }
}]);
