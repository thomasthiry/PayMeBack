angular.module('PayMeBack').service('authService', ['$q', '$http', 'backendHostUrl', authService]);

function authService($q, $http, backendHostUrl) {
    // copied from https://devdactic.com/user-auth-angularjs-ionic/ (contains roles management too)
    var LOCAL_TOKEN_KEY = 'AUTH_TOKEN';
    var httpAuthenticationHeader = 'Authentication';
    //var username = '';
    var isAuthenticated = false;
    var authToken;

    function loadUserCredentials() {
        var token = localStorage.getItem(LOCAL_TOKEN_KEY);
        if (token) {
            useCredentials(token);
        }
    }

    function storeUserCredentials(email, token) {
        localStorage.setItem(LOCAL_TOKEN_KEY, token);
        useCredentials(token);
    }

    function useCredentials(token) {
        //username = token.split('.')[0];
        isAuthenticated = true;
        authToken = token;

        // Set the token as header for your requests!
        $http.defaults.headers.common[httpAuthenticationHeader] = 'Bearer ' + token;
    }

    function destroyUserCredentials() {
        authToken = undefined;
        //username = '';
        isAuthenticated = false;
        $http.defaults.headers.common[httpAuthenticationHeader] = undefined;
        localStorage.removeItem(LOCAL_TOKEN_KEY);
    }

    var login = function (email, password) {
        return $q(function (resolve, reject) {
            var loginData = { email: email, password: password };
            $http.post(backendHostUrl + '/login', loginData).then(
                function(response) {
                    storeUserCredentials(email, response.data.token);
                    resolve('Login success.');
                },
                function () {
                    reject('Login Failed.');
                }
            );
        });
    };

    var logout = function () {
        destroyUserCredentials();
    };

    loadUserCredentials();

    return {
        login: login,
        logout: logout,
        isAuthenticated: function () { return isAuthenticated; },
        //username: function () { return username; },
    };
};
