angular.module('PayMeBack').controller('SplitContactViewController', ['$scope', '$state', '$stateParams', '$ionicPopup', 'contactService', function SplitContactViewController($scope, $state, $stateParams, $ionicPopup, contactService) {

    var showErrorPopup = function (response) {
        var alertPopup = $ionicPopup.alert({
            title: 'Connection failed',
            template: 'Please check your network'
        });
    };

    contactService.getSplitContactById($stateParams.splitId, $stateParams.splitContactId).then(
        function (splitContact) {
            splitContact.splitId = $stateParams.splitId;
            splitContact.id = $stateParams.splitContactId;
            $scope.splitContact = splitContact;
        }, showErrorPopup);

    $scope.save_click = function () {
        contactService.updateSplitContact($scope.splitContact).then(
            function () {
                $state.go('splitView', { splitId: $stateParams.splitId });
            }, showErrorPopup);
    };
}]);
