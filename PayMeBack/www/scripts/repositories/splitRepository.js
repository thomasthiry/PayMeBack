angular.module('PayMeBack').factory('splitRepository', function () {
    var _splits = [];
    var StorageKeySplits = 'splits';

    function getLastestId() {
        var ids = _splits.map(function (currentValue, index, array) { return currentValue.id });
        var maxId = Math.max.apply(Math, ids);

        return maxId > 0 ? maxId : 0;
    }

    function createSplit(fields) {
        var split = new Split();
        for (var key in fields) {
            //copy all the fields
            split[key] = fields[key];
        }

        return split;
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
            _splits = [];
            var storageString = localStorage.getItem(StorageKeySplits);
            if (storageString != null) {
                var splitFieldsArray = angular.fromJson(storageString);
                for (splitFields of splitFieldsArray) {
                    _splits.push(createSplit(splitFields));
                }
            }
        },
        saveToStorage: function () {
            localStorage.setItem(StorageKeySplits, angular.toJson(_splits));
        }
    };
});