angular.module('PayMeBack').controller('SplitContactViewController', ['$scope', '$state', '$stateParams', 'contactService', function SplitContactViewController($scope, $state, $stateParams, contactService) {

    contactService.getSplitContactById($stateParams.splitId, $stateParams.splitContactId).then(function (splitContact) {
        splitContact.splitId = $stateParams.splitId;
        splitContact.id = $stateParams.splitContactId;
        $scope.splitContact = splitContact;
    });

    $scope.save_click = function () {
        contactService.updateSplitContact($scope.splitContact).then(function() {
            $state.go('splitView', { splitId: $stateParams.splitId });
        });

        //$state.transitionTo('splitView', { splitId: $stateParams.splitId }, { reload: true, inherit: true, notify: true });
    };
}]);
