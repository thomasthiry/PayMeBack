angular.module('PayMeBack').controller('SplitListController', ['$scope', '$state', 'splitService', function ($scope, $state, splitService) {
    splitService.list().then(function (splits) {
        $scope.splits = splits;
    });

    //$scope.add_click = function () {
    //    var newSplit = splitService.create();
    //    $state.go('splitView', {splitId: newSplit.id});
    //};
}]);