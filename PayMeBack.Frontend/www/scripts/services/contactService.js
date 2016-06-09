angular.module('PayMeBack').service('contactService', ['backendHostUrl', '$http', '$cordovaContacts', '$ionicPlatform', contactService]);

function contactService(backendHostUrl, $http, $cordovaContacts, $ionicPlatform) {


    this.getBySplitId = function (splitId) {
        return $http.get(backendHostUrl + '/splits/' + splitId + '/contacts').then(
            function successCallback(response) {
                return response.data;
            }, _errorCallback);
    };

    this.createIfNeededAndAddToSplit = function (splitId, contactEmailToAdd, contactNameToAdd) {
        var contactToCreate = { email: contactEmailToAdd, splitId: splitId, name: contactNameToAdd };
        return $http.post(backendHostUrl + '/splits/' + splitId + '/contacts', contactToCreate).then(
            function successCallback(response) {
                return response.data;
            }, _errorCallback);
    };

    this.getSplitContactById = function (splitId, splitContactId) {
        return $http.get(backendHostUrl + '/splits/' + splitId + '/contacts/' + splitContactId).then(
            function successCallback(response) {
                return response.data;
            }, _errorCallback);
    };

    this.updateSplitContact = function (splitContact) {
        var data = { owes: splitContact.owes, paid: splitContact.paid, iban: splitContact.iban, address: splitContact.address, comments: splitContact.comments };
        return $http.put(backendHostUrl + '/splits/' + splitContact.splitId + '/contacts/' + splitContact.id, data).then(
            function successCallback(response) {
                return response.data;
            }, _errorCallback);
    };

    this.searchPhoneContacts = function (searchTerm) {
        // Used for debugging on Chrome
        //return { then: function (callback) { callback([{ displayName: 'Olivier Roger', emails: [{ value: 'olivier.roger@gmail.com' }] }, { displayName: 'Olivier Desvachez', emails: [{ value: 'olivier.desvachez@gmail.com' }] }]) } };

        //$ionicPlatform.ready(function () { // disabled because it doesn't return a promise
        var options = {
            filter: searchTerm, // 'Bob'
            multiple: true, // Yes, return any contact that matches criteria.
            fields: ['displayName', 'name', 'emails'], // These are the fields to search for 'bob'.
            desiredFields: ['displayName', 'name', 'emails'] //return fields, others will be null.
        };
        return $cordovaContacts.find(options);
        //});
    };

    function _errorCallback(response) {
        return $q.reject();
    }

}