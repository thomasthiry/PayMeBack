angular.module('PayMeBack').controller('SplitListController', ['$scope', '$state', '$ionicPopup', 'splitService', function ($scope, $state, $ionicPopup, splitService) {

    var showErrorPopup = function (response) {
        var alertPopup = $ionicPopup.alert({
            title: 'Connection failed',
            template: 'Please check your network'
        });
    };

    splitService.list().then(
        function (splits) {
            $scope.splits = splits;
        }, showErrorPopup);

    $scope.add_click = function () {
        var newSplit;
        splitService.create().then(
            function (split) {
                newSplit = split;
                $state.go('splitView', { splitId: newSplit.id });
            }, showErrorPopup);
    };
}]);