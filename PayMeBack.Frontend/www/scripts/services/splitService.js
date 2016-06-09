angular.module('PayMeBack').service('splitService', ['backendHostUrl', 'dateTimeProvider', '$http', '$q', splitService]);

function splitService(backendHostUrl, dateTimeProvider, $http, $q) {

    this.list = function () {
        return $http.get(backendHostUrl + '/splits').then(
            function successCallback(response) {
                return response.data;
            }, _errorCallback);
    };

    this.get = function (splitId) {
        return $http.get(backendHostUrl + '/splits/' + splitId).then(
            function successCallback(response) {
                return response.data;
            }, _errorCallback);
    };

    this.create = function () {
        var name = _formatDateTime(dateTimeProvider.now());
        var splitToCreate = { name: name, created: dateTimeProvider.now() };

        return $http.post(backendHostUrl + '/splits', JSON.stringify(splitToCreate)).then(
            function successCallback(response) {
                return response.data;
            }, _errorCallback);
    };

    this.getSettlement = function (splitId) {
        return $http.get(backendHostUrl + '/splits/' + splitId + '/settle').then(
            function successCallback(response) {
                return response.data;
            }, _errorCallback);
    };

    function _formatDateTime(date) {
        return date.toString().slice(0, 21);
    }

    function _errorCallback(response) {
        return $q.reject();
    }
}
