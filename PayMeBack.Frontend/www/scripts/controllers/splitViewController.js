angular.module('PayMeBack').controller('SplitViewController', ['$scope', '$state', '$stateParams', 'splitService', 'contactService', function SplitViewController($scope, $state, $stateParams, splitService, contactService) {

    $scope.form = { contactEmailToAdd: '' };

    splitService.get($stateParams.splitId).then(function (split) {
        $scope.split = split;
    });

    $scope.$on("$ionicView.enter", function (scopes, states) {
        $scope.refreshListOfContacts();
    });

    $scope.refreshListOfContacts = function () {
        contactService.getBySplitId($stateParams.splitId).then(function (splitContacts) {
            $scope.splitContacts = splitContacts;
        });
    }

    $scope.clickOnAddContactByEmail = function () {
        addContact($scope.split.id, $scope.form.contactEmailToAdd);
    }

    $scope.clickOnAutocompleteContact = function (autocompletePhoneContact) {
        addContact($scope.split.id, autocompletePhoneContact.emails[0].value, autocompletePhoneContact.displayName);
    }

    function addContact (splitId, contactEmail, contactName) {
        contactService.createIfNeededAndAddToSplit(splitId, contactEmail, contactName).then(function () {
            $scope.form.contactEmailToAdd = '';
            $scope.autocompleteContacts = [];

            $scope.refreshListOfContacts();
        });
    }

    $scope.goToSettle = function () {
        $state.go('settleView', { splitId: $scope.split.id });
    };

    $scope.searchPhoneContacts = function () {
        if ($scope.form.contactEmailToAdd.length < 2) {
            $scope.autocompleteContacts = [];
            return;
        }
            
        contactService.searchPhoneContacts($scope.form.contactEmailToAdd).then(function (returnedContacts) {
            $scope.autocompleteContacts = returnedContacts;
        });
    }
}]);
