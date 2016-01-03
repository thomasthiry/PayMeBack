angular.module('PayMeBack').controller('SplitViewController', ['$scope', '$stateParams', 'splitService', 'contactService', function SplitViewController($scope, $stateParams, splitService, contactService) {
    $scope.form = { contactEmailToAdd: '' };
    
    $scope.split = splitService.get($stateParams.splitId);
    
    function refreshListOfContacts() {
        $scope.contacts = contactService.list({ ids: $scope.split.contactIds });
    }
    refreshListOfContacts();

    $scope.addContactClick = function () {
        splitService.addContactToSplit($scope.split, $scope.form.contactEmailToAdd);

        $scope.form.contactEmailToAdd = '';

        refreshListOfContacts();
    }
}]);
