angular.module('PayMeBack').service('splitService', ['dateTimeProvider', 'splitRepository', 'contactRepository', splitService]);

function splitService(dateTimeProvider, splitRepository, contactRepository) {
    this.list = function () {
        return splitRepository.list();
    };

    this.get = function (splitId) {
        return splitRepository.get(splitId);
    };

    this.create = function() {
        var name = _formatDateTime(dateTimeProvider.now());
        var split = splitRepository.insert(name);

        splitRepository.saveToStorage();

        return split;
    };

    this.addContactToSplit = function (split, contactEmail) {
        var splitContact = contactRepository.get({ email: contactEmail });
        if (splitContact == null) {
            splitContact = new SplitContact(0, contactEmail);
            contactRepository.insert(splitContact);
        }
        split.addContact(splitContact.id);

        splitRepository.saveToStorage();
        contactRepository.saveToStorage();
    };

    function _formatDateTime(date) {
        return date.toString().slice(0, 21);
    }
}