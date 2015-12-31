angular.module('PayMeBack').factory('contactRepository', function () {
    var _contacts = [];
    var StorageKeyContact = 'contacts';

    function getNextId() {
        var ids = _contacts.map(function (currentValue, index, array) { return currentValue.id });
        var maxId = Math.max.apply(Math, ids);

        return maxId > 0 ? maxId + 1 : 1;
    }

    return {
        //list: function () {
        //    return _splits;
        //},
        get: function (searchQuery) {
            if (searchQuery.hasOwnProperty('email')) {
                var matchingContacts = _contacts.filter(function (value, index, ar) { return value.email == searchQuery.email });
                return matchingContacts.length > 0 ? matchingContacts[0] : null;
            }
        },
        insert: function (splitContact) {
            splitContact.id = getNextId();

            _contacts.push(splitContact);

            return splitContact.id;
        },
        //loadFromStorage: function () {
        //    var storageString = localStorage.getItem(StorageKeySplits);
        //    if (storageString != null) {
        //        _splits = JSON.parse(storageString);
        //    }
        //},
        //saveToStorage: function () {
        //    localStorage.setItem(StorageKeySplits, JSON.stringify(_splits));
        //}
    };
});