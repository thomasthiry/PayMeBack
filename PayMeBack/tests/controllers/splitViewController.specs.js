describe('SplitViewController', function () {
    var $controller, splitViewController, splitServiceSpy, contactServiceSpy, $scope = {};
    var splitReturnedInCallback = { id: 2, name: 'Tomorrow' };
    var contactsReturnedInCallback = [{ id: 1, name: 'Olivier' }, { id: 2, name: 'John' }];

    beforeEach(function () {
        module('PayMeBack');

        splitServiceSpy = jasmine.createSpyObj('splitServiceSpy', ['get']);
        splitServiceSpy.get.and.returnValue({ then: function (callback) { return callback(splitReturnedInCallback); } });

        contactServiceSpy = jasmine.createSpyObj('contactServiceSpy', ['getBySplitId']);
        contactServiceSpy.getBySplitId.and.returnValue({ then: function (callback) { return callback(contactsReturnedInCallback); } });

        module(function ($provide) {
            $provide.value('splitService', splitServiceSpy);
            $provide.value('contactService', contactServiceSpy);
            $provide.value('$stateParams', { splitId: 2 });
        });

        inject(function ($injector, _$controller_) {
            $controller = _$controller_;
        });

        splitViewController = $controller('SplitViewController', { $scope: $scope });
    });

    describe('controller initialization', function () {
        it('should fetch and set the split', function () {
            expect(splitServiceSpy.get).toHaveBeenCalledWith(splitReturnedInCallback.id);
            expect($scope.split.name).toEqual('Tomorrow');
        });
        it('should fetch and set the split contacts', function () {
            expect(contactServiceSpy.getBySplitId).toHaveBeenCalledWith(splitReturnedInCallback.id);
            expect($scope.splitContacts.length).toBeGreaterThan(1);
        });
    });

    //var $controller;
    //var $scope = {};
    //var splitViewController;
    //var split = new Split(5, 'Saturday');
    //split.contactIds = [1, 2];
    //var contacts = [new SplitContact(1, 'john@user.com'), new SplitContact(2, 'mark@user.com')];

    //beforeEach(function () {
    //    module('PayMeBack');

    //    splitServiceSpy = {
    //        get: jasmine.createSpy().and.returnValue(split),
    //        addContactToSplit: jasmine.createSpy(),
    //    };
    //    contactServiceSpy = {
    //        list: jasmine.createSpy().and.returnValue(contacts),
    //    };

    //    module(function ($provide) {
    //        $provide.value('splitService', splitServiceSpy);
    //        $provide.value('contactService', contactServiceSpy);
    //        $provide.value('$stateParams', { splitId: 5 });
    //    });

    //    inject(function ($injector, _$controller_) {
    //        $controller = _$controller_;
    //    });

    //    splitViewController = $controller('SplitViewController', { $scope: $scope });
    //});

    //describe('controller initialization', function () {
    //    it('should fetch the split', function () {
    //        expect(splitServiceSpy.get).toHaveBeenCalledWith(5);
    //    });
    //    it('should fetch the contacts of the split and store it in scope', function () {
    //        expect(contactServiceSpy.list).toHaveBeenCalledWith({ ids: split.contactIds });
    //        expect($scope.contacts).toEqual(contacts);
    //    });
    //});

    //describe('add contact to split', function () {
    //    var emailToAdd = 'sue@user.com'
    //    beforeEach(function () {
    //        $scope.form.contactEmailToAdd = emailToAdd;
    //        $scope.addContactClick();
    //    });

    //    it('should call the split service', function () {
    //        expect(splitServiceSpy.addContactToSplit).toHaveBeenCalledWith(split, emailToAdd);
    //    });

    //    it('should empty the textbox', function () {
    //        expect($scope.form.contactEmailToAdd).toEqual('');
    //    });
    //});
});