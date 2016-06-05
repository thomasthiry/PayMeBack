﻿angular.module('PayMeBack').service('contactService', ['backendHostUrl', '$http', contactService]);

function contactService(backendHostUrl, $http) {
    this.getBySplitId = function (splitId) {
        return $http.get(backendHostUrl + '/splits/' + splitId + '/contacts').then(
            function successCallback(response) {
                return response.data;
            }, _errorCallback);
    };

    this.createIfNeededAndAddToSplit = function (splitId, contactEmailToAdd) {
        var contactToCreate = { email: contactEmailToAdd, splitId: splitId };
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

    function _errorCallback(response) {
        alert('Error connecting. Details: ' + response.status + ' - ' + response.statusText);
    }
}