// Catches server auth errors to handle them

angular.module('PayMeBack').factory('authInterceptor', function ($rootScope, $q) {
    return {
        responseError: function (response) {
            if (response.status == 401 || response.status == 403) {
                $rootScope.$broadcast('auth-not-authenticated', response);
            }
            return $q.reject(response);
        }
    };
});

angular.module('PayMeBack').config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptor');
});