'use strict';

var app = angular.module('PayMeBack', ['ionic']);

app.config(function ($stateProvider, $urlRouterProvider) {
    $stateProvider
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

    $urlRouterProvider.otherwise('/splits');
});

app.value('backendHostUrl', 'http://192.168.1.100:62487');
app.value('dateTimeProvider', { now: function () { return new Date(); } });
