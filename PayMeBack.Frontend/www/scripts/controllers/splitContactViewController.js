angular.module('PayMeBack').controller('SplitContactViewController', ['$scope', '$stateParams', 'contactService', function SplitContactViewController($scope, $stateParams, contactService) {

    contactService.getSplitContactById($stateParams.splitId, $stateParams.splitContactId).then(function (splitContact) {
        splitContact.splitId = $stateParams.splitId;
        splitContact.id = $stateParams.splitContactId;
        $scope.splitContact = splitContact;
    });

    $scope.save_click = function () {
        contactService.updateSplitContact($scope.splitContact);
    };
}]);
