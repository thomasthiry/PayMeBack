angular.module('PayMeBack').controller('SplitContactViewController', ['$scope', '$stateParams', 'contactService', function SplitContactViewController($scope, $stateParams, contactService) {

    contactService.getSplitContactById($stateParams.splitId, $stateParams.splitContactId).then(function (splitContact) {
        $scope.splitContact = splitContact;
    });

    $scope.save_click = function () {
        contactService.updateSplitContact($stateParams.splitId, $stateParams.splitContactId, $scope.splitContact.owes, $scope.splitContact.paid, $scope.splitContact.comments);
    };
}]);
