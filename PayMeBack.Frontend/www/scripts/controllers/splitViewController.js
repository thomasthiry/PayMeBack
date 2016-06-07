angular.module('PayMeBack').controller('SplitViewController', ['$scope', '$state', '$stateParams', 'splitService', 'contactService', function SplitViewController($scope, $state, $stateParams, splitService, contactService) {
    
    $scope.$on("$ionicView.enter", function (scopes, states) {
        $scope.refreshListOfContacts();
    });

    $scope.refreshListOfContacts = function () {
        contactService.getBySplitId($stateParams.splitId).then(function (splitContacts) {
            $scope.splitContacts = splitContacts;
        });
    }

    $scope.addContact = function () {
        contactService.createIfNeededAndAddToSplit($scope.split.id, $scope.form.contactEmailToAdd).then(function () {
            $scope.form.contactEmailToAdd = '';

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
            $scope.autocompleteContacts = returnedContacts; console.log($scope.autocompleteContacts);
        });
    }

    $scope.form = { contactEmailToAdd: '' };

    splitService.get($stateParams.splitId).then(function (split) {
        $scope.split = split;
    });
}]);
