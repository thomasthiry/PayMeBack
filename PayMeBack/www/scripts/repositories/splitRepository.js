angular.module('PayMeBack').factory('splitRepository', function () {
    var _splits = [];
    var StorageKeySplits = 'splits';

    function getLastestId() {
        var ids = _splits.map(function (currentValue, index, array) { return currentValue.id });
        var maxId = Math.max.apply(Math, ids);

        return maxId > 0 ? maxId : 0;
    }

    return {
        list: function () {
            return _splits;
        },
        get: function (splitId) {
            var matchingSplits = _splits.filter(function (value, index, ar) { return value.id == splitId });
            return matchingSplits.length > 0 ? matchingSplits[0] : null;
        },
        insert: function (name) {
            var split = new Split(getLastestId() + 1, name);

            _splits.push(split);

            return split;
        },
        loadFromStorage: function () {
            var storageString = localStorage.getItem(StorageKeySplits);
            if (storageString != null) {
                _splits = JSON.parse(storageString);
            }
        },
        saveToStorage: function () {
            localStorage.setItem(StorageKeySplits, JSON.stringify(_splits));
        }
    };
});