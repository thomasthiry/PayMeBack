'use strict';

var app = angular.module('PayMeBack', ['ionic']);

app.config(function ($stateProvider, $urlRouterProvider) {
    $stateProvider
        .state('login', {
            url: '/login',
            templateUrl: 'views/login.html',
            controller: 'LoginController'
        })
        .state('splitList', {
            url: '/splits',
            templateUrl: 'views/splitList.html',
            controller: 'SplitListController'
        })
        .state('splitView', {
            url: '/splits/:splitId',
            templateUrl: 'views/splitView.html',
            controller: 'SplitViewController'
        })
        .state('splitContactView', {
            url: '/splits/:splitId/contact/:splitContactId',
            templateUrl: 'views/splitContactView.html',
            controller: 'SplitContactViewController'
        })
        .state('settleView', {
            url: '/splits/:splitId/settle',
            templateUrl: 'views/settleView.html',
            controller: 'SettleViewController'
        });

    $urlRouterProvider.otherwise('/login');
});

app.value('backendHostUrl', 'http://192.168.1.100:62487');
app.value('dateTimeProvider', { now: function () { return new Date(); } });

// Check authentication on page change
app.run(function ($rootScope, $state, authService) {
    $rootScope.$on('$stateChangeStart', function (event, next, nextParams, fromState) {
        if (!authService.isAuthenticated()) {
            if (next.name !== 'login') {
                event.preventDefault();
                $state.go('login');
            }
        }
    });
})