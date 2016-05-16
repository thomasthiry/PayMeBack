angular.module('PayMeBack').controller('SettleViewController', ['$scope', '$stateParams', 'splitService', function SettleViewController($scope, $stateParams, splitService) {

    splitService.getSettlement($stateParams.splitId).then(function (settlement) {
        $scope.settlement = settlement;
    });
}]);
