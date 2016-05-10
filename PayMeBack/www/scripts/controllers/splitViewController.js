angular.module('PayMeBack').controller('SplitViewController', ['$scope', '$state', '$stateParams', 'splitService', 'contactService', function SplitViewController($scope, $state, $stateParams, splitService, contactService) {
    $scope.form = { contactEmailToAdd: '' };

    splitService.get($stateParams.splitId).then(function (split) {
        $scope.split = split;
    });

    refreshListOfContacts();

    function refreshListOfContacts() {
        contactService.getBySplitId($stateParams.splitId).then(function (splitContacts) {
            $scope.splitContacts = splitContacts;
        });
    }

    $scope.addContact = function () {
        contactService.createIfNeededAndAddToSplit($scope.split.id, $scope.form.contactEmailToAdd).then(function () {
            $scope.form.contactEmailToAdd = '';

            refreshListOfContacts();
        });
    }

    $scope.goToSettle = function () {
        $state.go('settleView', { splitId: $scope.split.id });
    };
}]);
