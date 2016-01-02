angular.module('PayMeBack').factory('splitService', ['dateTimeProvider', 'splitRepository', 'contactRepository', function (dateTimeProvider, splitRepository, contactRepository) {
    function formatDateTime(date) {
        return date.toString().slice(0, 21);
    }

    return {
        list: function () {
            return splitRepository.list()
        },
        get: function (splitId) {
            return splitRepository.get(splitId);
        },
        create: function() {
            var name = formatDateTime(dateTimeProvider.now());
            var split = splitRepository.insert(name);

            splitRepository.saveToStorage();

            return split;
        },
        addContactToSplit: function (split, contactEmail) {
            var splitContact = contactRepository.get({ email: contactEmail });
            if (splitContact == null) {
                splitContact = new SplitContact(0, contactEmail);
                splitContact.id = contactRepository.insert(splitContact);
            }

            split.addContact(splitContact);

            splitRepository.saveToStorage();
            contactRepository.saveToStorage();
        },
    };
}]);