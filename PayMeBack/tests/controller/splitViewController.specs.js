describe('SplitViewController', function () {
    var $controller;
    var $scope = {};
    var splitViewController;
    var split = new Split(5, 'Saturday');
    split.contactIds = [1, 2];
    var contacts = [new SplitContact(1, 'john@user.com'), new SplitContact(2, 'mark@user.com')];

    beforeEach(function () {
        module('PayMeBack');

        splitServiceSpy = {
            get: jasmine.createSpy().and.returnValue(split),
            addContactToSplit: jasmine.createSpy(),
        };
        contactServiceSpy = {
            list: jasmine.createSpy().and.returnValue(contacts),
        };

        module(function ($provide) {
            $provide.value('splitService', splitServiceSpy);
            $provide.value('contactService', contactServiceSpy);
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
        it('should fetch the contacts of the split and store it in scope', function () {
            expect(contactServiceSpy.list).toHaveBeenCalledWith({ ids: split.contactIds });
            expect($scope.contacts).toEqual(contacts);
        });
    });

    describe('add contact to split', function () {
        var emailToAdd = 'sue@user.com'
        beforeEach(function () {
            $scope.form.contactEmailToAdd = emailToAdd;
            $scope.addContactClick();
        });

        it('should call the split service', function () {
            expect(splitServiceSpy.addContactToSplit).toHaveBeenCalledWith(split, emailToAdd);
        });

        it('should empty the textbox', function () {
            expect($scope.form.contactEmailToAdd).toEqual('');
        });
    });
});