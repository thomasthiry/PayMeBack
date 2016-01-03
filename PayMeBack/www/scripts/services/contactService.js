angular.module('PayMeBack').service('contactService', ['contactRepository', contactService]);

function contactService(contactRepository) {
    this.list = function (query) {
        return contactRepository.list(query);
    };
}