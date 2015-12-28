angular.module('PayMeBack').controller('splitView', ['$scope', '$stateParams', 'splitService', function ($scope, $stateParams, splitService) {
    $scope.split = splitService.get($stateParams.splitId);
}]);
