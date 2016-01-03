angular.module('PayMeBack').service('splitRepository', splitRepository);

function splitRepository() {
    var _splits = [];
    var _storageKeySplits = 'splits';

    this.list = function () {
        return _splits;
    };

    this.get = function (splitId) {
        var matchingSplits = _splits.filter(function (value, index, ar) { return value.id == splitId });
        return matchingSplits.length > 0 ? matchingSplits[0] : null;
    };

    this.insert = function (name) {
        var split = new Split(_getLastestId() + 1, name);

        _splits.push(split);

        return split;
    };

    this.loadFromStorage = function () {
        _splits = [];
        var storageString = localStorage.getItem(_storageKeySplits);
        if (storageString != null) {
            var splitFieldsArray = angular.fromJson(storageString);
            for (splitFields of splitFieldsArray) {
                _splits.push(_createSplit(splitFields));
            }
        }
    };

    this.saveToStorage = function () {
        localStorage.setItem(_storageKeySplits, angular.toJson(_splits));
    };

    function _getLastestId() {
        var ids = _splits.map(function (currentValue, index, array) { return currentValue.id });
        var maxId = Math.max.apply(Math, ids);

        return maxId > 0 ? maxId : 0;
    }

    function _createSplit(fields) {
        var split = new Split();
        for (var key in fields) {
            split[key] = fields[key]; //copy all the fields
        }
        return split;
    }
}