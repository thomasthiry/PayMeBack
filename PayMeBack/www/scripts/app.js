'use strict';

var app = angular.module('PayMeBack', ['ionic']);

app.config(function ($stateProvider, $urlRouterProvider) {
    $stateProvider
        .state('splitList', {
            url: '/splits',
            templateUrl: 'views/splitList.html',
            controller: 'splitList'
        })
        .state('splitView', {
            url: '/splits/:splitId',
            templateUrl: 'views/splitView.html',
            controller: 'splitView'
        });

    $urlRouterProvider.otherwise('/splits');
});

app.value('dateTimeProvider', { now: function () { return new Date(); } });