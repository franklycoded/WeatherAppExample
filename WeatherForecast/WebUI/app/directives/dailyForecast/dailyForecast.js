var weatherApp = angular.module('WeatherApp');

weatherApp.directive('dailyForecast', function () {
    return {
        restrict: 'E',
        scope: {
            forecast: '=forecast'
        },
        templateUrl: '/WebUI/app/directives/dailyForecast/dailyForecast.html'
    };
});