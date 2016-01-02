angular.module('PayMeBack').controller('SplitListController', ['$scope', '$state', 'splitService', function ($scope, $state, splitService) {
    $scope.splits = splitService.list();

    $scope.add_click = function () {
        var newSplit = splitService.create();
        $state.go('splitView', {splitId: newSplit.id});
    };
}]);