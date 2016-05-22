angular.module('PayMeBack').controller('LoginController', function ($scope, $state, $ionicPopup, authService) {

    $scope.data = {};

    $scope.login = function (data) {
        authService.login(data.username, data.password).then(
            function (authenticated) {
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