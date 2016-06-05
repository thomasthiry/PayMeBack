angular.module('PayMeBack').controller('AppController', function ($scope, $state, $ionicPopup, authService) {

    //$scope.username = authService.username();
 
    $scope.$on('auth-not-authenticated', function (event) {
        authService.logout();
        $state.go('login');
        var alertPopup = $ionicPopup.alert({
            title: 'Session Lost!',
            template: 'Sorry, You have to login again.'
        });
    });
 
    $scope.setCurrentUsername = function(name) {
        $scope.username = name;
    };
})