angular.module('PayMeBack').factory('splitService', ['dateTimeProvider', 'splitRepository', function (dateTimeProvider, splitRepository) {
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
            var date = formatDateTime(dateTimeProvider.now());
            return splitRepository.insert(date);
        },
    };
}]);