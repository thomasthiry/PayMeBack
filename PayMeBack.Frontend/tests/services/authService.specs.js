//var authService;
//var localStorageSpy;

//describe('AuthService', function () {
//    beforeEach(function () {
//        module('PayMeBack');
        
//        //localStorageSpy = jasmine.createSpy('storage');//jasmine.createSpyObj('localStorageSpy', ['getItem']);

//        //module(function ($provide) {
//        //    $provide.value('storage', localStorageSpy);
//        //});

//        inject(function ($injector) {
//            authService = $injector.get('authService');
//            $httpBackend = $injector.get('$httpBackend');
//            //backendHostUrl = $injector.get('backendHostUrl');
//            $httpBackend.when('GET', /^views\//).respond(null); // For some reason a call to a view is in the queue of the $httpBackend... this is a 'temporary' workaround
//        });

//        //authService.logout();
//    });

//    describe('login', function () {

//        it('should not be authenticated when started', function () {
//            //var tokenStub = 'thomas.OIFJIOZEJFREZAREZORIOE';
//            //$httpBackend.when('POST', backendHostUrl + '/login').respond(401, { message: "Not Authenticated" });
//            //var _token;
//            //authService.login('thomas', 'mypass').then(function (token) {
//            //    _token = token;
//            //});
//            //$httpBackend.flush();
//            localStorageSpy.getItem.and.returnValue(null); //'thomas.OIFJIOZEJFREZAREZORIOE'

//            expect(authService.isAuthenticated()).toBeFalsy();
//        });

//        //it('should call the web service', function () {
//        //    var tokenStub = 'thomas.OIFJIOZEJFREZAREZORIOE';
//        //    $httpBackend.expect('POST', backendHostUrl + '/login').respond({ token: tokenStub });
//        //    var _token;
//        //    authService.login('thomas', 'mypass').then(function (token) {
//        //        _token = token;
//        //    });
//        //    $httpBackend.flush();
//        //});

//        //it('should be authenticated if login successful', function () {
//        //    var tokenStub = 'thomas.OIFJIOZEJFREZAREZORIOE';
//        //    $httpBackend.when('POST', backendHostUrl + '/login').respond({ token: tokenStub });
//        //    var _token;
//        //    authService.login('thomas', 'mypass').then(function (token) {
//        //        _token = token;
//        //    });
//        //    $httpBackend.flush();

//        //    expect(authService.isAuthenticated()).toBeTruthy();
//        //});

//        it('should not be authenticated if login unsuccessful', function () {
//            //var tokenStub = 'thomas.OIFJIOZEJFREZAREZORIOE';
//            //$httpBackend.when('POST', backendHostUrl + '/login').respond(401, { message: "Not Authenticated" });
//            //var _token;
//            //authService.login('thomas', 'mypass').then(function (token) {
//            //    _token = token;
//            //});
//            //$httpBackend.flush();

//            expect(authService.isAuthenticated()).toBeFalsy();
//        });
//    });
//});