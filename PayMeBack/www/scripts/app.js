'use strict';

var app = angular.module('PayMeBack', ['ionic', 'ngResource']);

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
        });

    $urlRouterProvider.otherwise('/splits');
});

app.value('backendHostUrl', 'http://localhost:62487');
app.value('dateTimeProvider', { now: function () { return new Date(); } });