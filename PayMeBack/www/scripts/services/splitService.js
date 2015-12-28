angular.module('PayMeBack').factory('splitService', ['splitRepository', function (splitRepository) {
    function formatDateTime(date) {
        return date.toISOString().slice(0, 16).replace('T', ' ');
    }

    return {
        list: function () {
            return splitRepository.list()
        },
        get: function (splitId) {
            return splitRepository.get(splitId);
        },
        createSplit: function() {
            var date = formatDateTime(new Date());
            return splitRepository.insert(date);
        },
    };
}]);