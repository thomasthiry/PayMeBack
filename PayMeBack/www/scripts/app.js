var app = angular.module('PayMeBack', ['ionic']);

app.config(function ($stateProvider, $urlRouterProvider) {
    $stateProvider
        .state('splitList', {
            url: '/splits',
            templateUrl: 'views/splitList.html',
            controller: 'splitList'
        })
        .state('splitView', {
            url: '/splits/:splitId',
            templateUrl: 'views/splitView.html',
            controller: 'splitView'
        });

    $urlRouterProvider.otherwise('/splits');
});

app.controller('splitList', ['$scope', 'splitService', function ($scope, splitService) {
    $scope.splits = splitService.list();
}]);

app.controller('splitView', ['$scope', '$stateParams', 'splitService', function ($scope, $stateParams, splitService) {
    $scope.split = splitService.get($stateParams.splitId);
}]);

app.factory('splitService', function() {
    var _splits = [{ id: 1, date: 'yesterday' }, { id: 2, date: 'today' }, { id: 3, date: 'Sat 25 Oct 2015' }];
    return {
        list: function () {
            return _splits;
        },
        get: function (splitId) {
            var matchingSplits = _splits.filter(function (value, index, ar) { return value.id == splitId });
            return matchingSplits.length > 0 ? matchingSplits[0] : null;
        }
    };
});