﻿angular.module('PayMeBack').controller('SplitContactViewController', ['$scope', '$stateParams', 'contactService', function SplitContactViewController($scope, $stateParams, contactService) {
    contactService.getSplitContactById($stateParams.splitContactId).then(function (splitContact) {
        $scope.splitContact = splitContact;
    });
}]);
