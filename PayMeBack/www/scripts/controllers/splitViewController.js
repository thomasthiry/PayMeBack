angular.module('PayMeBack').controller('SplitViewController', ['$scope', '$stateParams', 'splitService', 'contactService', function SplitViewController($scope, $stateParams, splitService, contactService) {
    $scope.form = { contactEmailToAdd: '' };

    splitService.get($stateParams.splitId).then(function (split) {
        $scope.split = split;
    });

    contactService.getBySplitId($stateParams.splitId).then(function (contacts) {
        $scope.splitContacts = contacts;
    });
    
    //function refreshListOfContacts() {
    //    $scope.contacts = contactService.list({ ids: $scope.split.contactIds });
    //}
    //refreshListOfContacts();

    //$scope.addContactClick = function () {
    //    splitService.addContactToSplit($scope.split, $scope.form.contactEmailToAdd);

    //    $scope.form.contactEmailToAdd = '';

    //    refreshListOfContacts();
    //}
}]);
