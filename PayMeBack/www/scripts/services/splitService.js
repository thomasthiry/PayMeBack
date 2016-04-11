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

    //this.create = function() {
    //    var name = _formatDateTime(dateTimeProvider.now());
    //    var split = new Split(null, name);

    //    splitRepository.saveToStorage();

    //    return split;
    //};

    //this.addContactToSplit = function (split, contactEmail) {
    //    var splitContact = contactRepository.get({ email: contactEmail });
    //    if (splitContact == null) {
    //        splitContact = new SplitContact(0, contactEmail);
    //        contactRepository.insert(splitContact);
    //    }
    //    split.addContact(splitContact.id);

    //    splitRepository.saveToStorage();
    //    contactRepository.saveToStorage();
    //};

    //function _formatDateTime(date) {
    //    return date.toString().slice(0, 21);
    //}
}