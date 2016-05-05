describe('SplitViewController', function () {
    var $controller, splitViewController, splitServiceSpy, contactServiceSpy, $scope = {};
    var splitReturnedInCallback = { id: 2, name: 'Tomorrow' };
    var contactsReturnedInCallback = [{ id: 1, email: 'olivier@gmail.com' }, { id: 2, email: 'john@gmail.com' }];
    var splitIdInState = 2;

    beforeEach(function () {
        module('PayMeBack');

        splitServiceSpy = jasmine.createSpyObj('splitServiceSpy', ['get']);
        splitServiceSpy.get.and.returnValue({ then: function (callback) { return callback(splitReturnedInCallback); } });

        contactServiceSpy = jasmine.createSpyObj('contactServiceSpy', ['getBySplitId', 'createIfNeededAndAddToSplit']);
        contactServiceSpy.getBySplitId.and.returnValue({ then: function (callback) { return callback(contactsReturnedInCallback); } });

        module(function ($provide) {
            $provide.value('splitService', splitServiceSpy);
            $provide.value('contactService', contactServiceSpy);
            $provide.value('$stateParams', { splitId: splitIdInState });
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

    describe('click on add contact', function () {
        var contactEmailToAdd = 'sam@gmail.com';
        var newContactReturnedInCallback = { id: 3, email: contactEmailToAdd }
        beforeEach(function () {
            $scope.form.contactEmailToAdd = contactEmailToAdd;
            contactServiceSpy.createIfNeededAndAddToSplit.and.returnValue({ then: function (callback) { return callback(newContactReturnedInCallback); } });
            $scope.splitContacts = [];

            $scope.addContact();
        });

        it('should call createIfNeededAndAddToSplit on the contact service', function () {
            expect(contactServiceSpy.createIfNeededAndAddToSplit).toHaveBeenCalledWith(splitIdInState, contactEmailToAdd);
        });
        it('should fetch and set the split again', function () {
            expect($scope.splitContacts.length).toBeGreaterThan(1);
        });
        it('should empty the textbox', function () {
            expect($scope.form.contactEmailToAdd).toEqual('');
        });
    });
});