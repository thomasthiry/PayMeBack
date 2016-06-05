angular.module('PayMeBack').controller('LoginController', function ($scope, $state, $ionicPopup, $ionicHistory, authService) {

    $scope.data = {};

    $scope.login = function (data) {
        authService.login(data.username, data.password).then(
            function (authenticated) {
                $ionicHistory.nextViewOptions({
                    disableBack: true
                });
                $state.go('splitList', {}, { reload: true });
                $scope.setCurrentUsername(data.username);
            },
            function (err) {
                var alertPopup = $ionicPopup.alert({
                    title: 'Login failed!',
                    template: 'Please check your credentials!'
                });
            });
    };
})