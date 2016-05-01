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
}