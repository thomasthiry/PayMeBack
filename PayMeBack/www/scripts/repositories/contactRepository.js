angular.module('PayMeBack').service('contactRepository', contactRepository);

function contactRepository() {
    var _contacts = [];
    var _storageKeyContact = 'contacts';

    this.list = function (searchQuery) {
        if (searchQuery != null && searchQuery.hasOwnProperty('ids')) {
            var matchingContacts = _contacts.filter(function (value, index, ar) { return searchQuery.ids.indexOf(value.id) > -1 });
            return matchingContacts;
        }
        return _contacts;
    };

    this.get = function (searchQuery) {
        if (searchQuery != null && searchQuery.hasOwnProperty('email')) {
            var matchingContacts = _contacts.filter(function (value, index, ar) { return value.email == searchQuery.email });
            return matchingContacts.length > 0 ? matchingContacts[0] : null;
        }
        return null;
    };

    this.insert = function (splitContact) {
        splitContact.id = _getNextId();
        _contacts.push(splitContact);

        return splitContact.id;
    };

    this.loadFromStorage = function () {
        _contacts = [];
        var storageString = localStorage.getItem(_storageKeyContact);
        if (storageString != null) {
            var contactFieldsArray = angular.fromJson(storageString);
            for (contactFields of contactFieldsArray) {
                _contacts.push(_createContact(contactFields));
            }
        }
    };

    this.saveToStorage = function () {
        localStorage.setItem(_storageKeyContact, angular.toJson(_contacts));
    };

    function _getNextId() {
        if (_contacts.length < 1) {
            return 1;
        }

        var ids = _contacts.map(function (currentValue, index, array) { return currentValue.id });
        var maxId = Math.max.apply(Math, ids);

        return maxId + 1;
    }

    function _createContact(fields) {
        var splitContact = new SplitContact();
        for (var key in fields) {
            splitContact[key] = fields[key]; //copy all the fields
        }
        return splitContact;
    }
}
