angular.module('PayMeBack').service('contactService', ['backendHostUrl', '$http', contactService]);

function contactService(backendHostUrl, $http) {
    this.getBySplitId = function (splitId) {
        return $http.get(backendHostUrl + '/splits/' + splitId + '/contacts').then(
            function successCallback(response) {
                return response.data;
            },
            function errorCallback(response) {
                console.log('failure');
            });
    };

    this.createIfNeededAndAddToSplit = function (splitId, contactEmailToAdd) {
        var contactToCreate = { email: contactEmailToAdd, splitId: splitId };
        return $http.post(backendHostUrl + '/splits/' + splitId + '/contacts', contactToCreate).then(
            function successCallback(response) {
                return response.data;
            },
            function errorCallback(response) {
                console.log('failure');
            });
    };

    this.getSplitContactById = function (splitId, splitContactId) {
        return $http.get(backendHostUrl + '/splits/' + splitId + '/contacts/' + splitContactId).then(
            function successCallback(response) {
                return response.data;
            },
            function errorCallback(response) {
                console.log('failure');
            });
    };
}