angular.module('PayMeBack').factory('splitRepository', function () {
    var _splits = [{ id: 1, date: 'yesterday' }, { id: 2, date: 'today' }, { id: 3, date: 'Sat 25 Oct 2015' }];

    function getLastestId() {
        var ids = _splits.map(function (currentValue, index, array) { return currentValue.id });
        var maxId = Math.max.apply(Math, ids);

        return maxId > 0 ? maxId : 1;
    }

    return {
        list: function () {
            return _splits;
        },
        get: function (splitId) {
            var matchingSplits = _splits.filter(function (value, index, ar) { return value.id == splitId });
            return matchingSplits.length > 0 ? matchingSplits[0] : null;
        },
        insert: function (date) {
            var split = { date: date, id: getLastestId() + 1 };

            _splits.push(split);

            return split;
        }
    };
});