var weatherApp = angular.module('WeatherApp');
weatherApp.factory('forecastService', ['$http', function ($http) {
    var service = {
        getForecast: getForecast
    };

    function getForecast(cityName) {
        return $http.get('/api/forecast/' + cityName);
    }

    return service;
}]);