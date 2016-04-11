angular.module('PayMeBack').controller('SplitViewController', ['$scope', '$stateParams', 'splitService', function SplitViewController($scope, $stateParams, splitService) {
    $scope.form = { contactEmailToAdd: '' };
    
    splitService.get($stateParams.splitId).then(function (split) {
        $scope.split = split;
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
