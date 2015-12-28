angular.module('PayMeBack').controller('splitList', ['$scope', '$state', 'splitService', function ($scope, $state, splitService) {
    $scope.splits = splitService.list();

    $scope.add_click = function () {
        var newSplit = splitService.createSplit();
        $state.go('splitView', {splitId: newSplit.id});
    };
}]);