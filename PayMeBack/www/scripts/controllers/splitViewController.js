angular.module('PayMeBack').controller('SplitViewController', ['$scope', '$stateParams', 'splitService', function SplitViewController($scope, $stateParams, splitService) {
    $scope.contactEmailToAdd = '';
    
    $scope.split = splitService.get($stateParams.splitId);

    this.addContactClick = function () {
        splitService.addContactToSplit($scope.split, $scope.contactEmailToAdd);
    }
}]);
