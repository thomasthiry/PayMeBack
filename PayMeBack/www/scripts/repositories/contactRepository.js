angular.module('PayMeBack').factory('contactRepository', function () {
    var _contacts = [];
    var StorageKeyContact = 'contacts';

    function _getNextId() {
        if (_contacts.length < 1) {
            return 1;
        }

        var ids = _contacts.map(function (currentValue, index, array) { return currentValue.id });
        var maxId = Math.max.apply(Math, ids);
        
        return maxId + 1;
    }

    function createContact(fields) {
        var splitContact = new SplitContact();
        for (var key in fields) {
            //copy all the fields
            splitContact[key] = fields[key];
        }

        return splitContact;
    }

    return {
        list: function (searchQuery) {
            if (searchQuery != null && searchQuery.hasOwnProperty('ids')) {
                var matchingContacts = _contacts.filter(function (value, index, ar) { return searchQuery.ids.indexOf(value.id) > -1 });
                return matchingContacts;
            }
            return _contacts;
        },
        get: function (searchQuery) {
            if (searchQuery != null && searchQuery.hasOwnProperty('email')) {
                var matchingContacts = _contacts.filter(function (value, index, ar) { return value.email == searchQuery.email });
                return matchingContacts.length > 0 ? matchingContacts[0] : null;
            }
            return null;
        },
        insert: function (splitContact) {
            splitContact.id = _getNextId();
            _contacts.push(splitContact);

            return splitContact.id;
        },
        loadFromStorage: function () {
            _contacts = [];
            var storageString = localStorage.getItem(StorageKeyContact);
            if (storageString != null) {
                var contactFieldsArray = angular.fromJson(storageString);
                for (contactFields of contactFieldsArray) {
                    _contacts.push(createContact(contactFields));
                }
            }
        },
        saveToStorage: function () {
            localStorage.setItem(StorageKeyContact, angular.toJson(_contacts));
        }
    };
});