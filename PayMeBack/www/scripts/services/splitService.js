angular.module('PayMeBack').service('splitService', ['backendHostUrl', 'dateTimeProvider', '$http', splitService]);

function splitService(backendHostUrl, dateTimeProvider, $http) {
    this.list = function () {
        return $http.get(backendHostUrl + '/splits').then(
            function successCallback(response) {
                return response.data;
            },
            function errorCallback(response) {
                console.log('failure');
            });
    };

    this.get = function (splitId) {
        return $http.get(backendHostUrl + '/splits/' + splitId).then(
            function successCallback(response) {
                return response.data;
            },
            function errorCallback(response) {
                console.log('failure');
            });
    };

    this.create = function () {
        var name = _formatDateTime(dateTimeProvider.now());
        var splitToCreate = { name: name, created: dateTimeProvider.now() };

        return $http.post(backendHostUrl + '/splits', JSON.stringify(splitToCreate)).then(
            function successCallback(response) {
                return response.data;
            },
            function errorCallback(response) {
                console.log('failure');
            });
    };

    this.getSettlement = function (splitId) {
        return $http.get(backendHostUrl + '/splits/' + splitId + '/settle').then(
            function successCallback(response) {
                return response.data;
            },
            function errorCallback(response) {
                console.log('failure');
            });
    };

    function _formatDateTime(date) {
        return date.toString().slice(0, 21);
    }
}