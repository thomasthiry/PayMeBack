angular.module('PayMeBack').controller('SettleViewController', ['$scope', '$stateParams', '$ionicPopup', 'splitService', function SettleViewController($scope, $stateParams, $ionicPopup, splitService) {

    var showErrorPopup = function (response) {
        var alertPopup = $ionicPopup.alert({
            title: 'Connection failed',
            template: 'Please check your network'
        });
    };

    splitService.getSettlement($stateParams.splitId).then(
        function (settlement) {
            $scope.settlement = settlement;
        }, showErrorPopup);
}]);
