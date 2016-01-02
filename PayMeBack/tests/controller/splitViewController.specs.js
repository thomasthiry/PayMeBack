describe('SplitViewController', function () {
    var $controller;
    var $scope = {};
    var splitViewController;
    var split = new Split(1, 'john@user.com');

    beforeEach(function () {
        module('PayMeBack');

        splitServiceSpy = {
            get: jasmine.createSpy().and.returnValue(split),
            addContactToSplit: jasmine.createSpy(),
        };

        module(function ($provide) {
            $provide.value('splitService', splitServiceSpy);
            $provide.value('$stateParams', { splitId: 5 });
        });

        inject(function ($injector, _$controller_) {
            $controller = _$controller_;
        });

        splitViewController = $controller('SplitViewController', { $scope: $scope });
    });

    describe('controller initialization', function () {
        it('should fetch the split', function () {
            expect(splitServiceSpy.get).toHaveBeenCalledWith(5);
        });
    });

    describe('add contact to split', function () {
        it('should call the split service', function () {
            $scope.contactEmailToAdd = split.email;
            splitViewController.addContactClick();
            expect(splitServiceSpy.addContactToSplit).toHaveBeenCalledWith(split, $scope.contactEmailToAdd);
        });
    });
});