angular.module('PayMeBack').factory('contactService', ['contactRepository', function (contactRepository) {
    return {
        list: function (query) {
            return contactRepository.list(query);
        },
    };
}]);