var weatherApp = angular.module('WeatherApp');

weatherApp.controller('WeatherMainController', ['$scope', 'forecastService', function ($scope, forecastService) {
    $scope.cityName = "";
    $scope.weatherData = {};
    $scope.errorMessage = "error message test";
    $scope.isErrorVisible = false;
    $scope.isTitleVisible = false;

    $scope.getForecast = function () {
        $scope.isErrorVisible = false;

        if ($scope.cityName != null && $scope.cityName != "") {
            forecastService.getForecast($scope.cityName)
            .then(function (weatherData) {
                $scope.weatherData = weatherData.data;
                $scope.isTitleVisible = true;
            },
            function (error) {
                console.log(error);
                $scope.isErrorVisible = true;
                $scope.errorMessage = "Error while getting weather data!";
            });
        }
    }
}]);
