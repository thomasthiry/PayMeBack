describe('SplitViewController', function () {
    var $controller, splitViewController, splitServiceSpy, contactServiceSpy, $scope, $state;
    var splitReturnedInCallback = { id: 2, name: 'Tomorrow' };
    var contactsReturnedInCallback = [{ id: 1, email: 'olivier@gmail.com' }, { id: 2, email: 'john@gmail.com' }];
    var splitIdInState = 2;

    beforeEach(function () {
        module('PayMeBack');

        splitServiceSpy = jasmine.createSpyObj('splitServiceSpy', ['get']);
        splitServiceSpy.get.and.returnValue({ then: function (callback) { return callback(splitReturnedInCallback); } });

        contactServiceSpy = jasmine.createSpyObj('contactServiceSpy', ['getBySplitId', 'createIfNeededAndAddToSplit', 'searchPhoneContacts']);
        contactServiceSpy.getBySplitId.and.returnValue({ then: function (callback) { return callback(contactsReturnedInCallback); } });

        $state = jasmine.createSpyObj('$state', ['go']);
        $scope = jasmine.createSpyObj('$scope', ['$on']);

        module(function ($provide) {
            $provide.value('splitService', splitServiceSpy);
            $provide.value('contactService', contactServiceSpy);
            $provide.value('$stateParams', { splitId: splitIdInState });
        });

        inject(function ($injector, _$controller_) {
            $controller = _$controller_;
        });

        splitViewController = $controller('SplitViewController', { $scope: $scope, $state: $state });
    });

    describe('controller initialization', function () {
        it('should fetch and set the split', function () {
            expect(splitServiceSpy.get).toHaveBeenCalledWith(splitReturnedInCallback.id);
            expect($scope.split.name).toEqual('Tomorrow');
        });
    });

    describe('click on add contact', function () {
        var contactEmailToAdd = 'sam@gmail.com';
        var newContactReturnedInCallback = { id: 3, email: contactEmailToAdd }
        beforeEach(function () {
            $scope.form.contactEmailToAdd = contactEmailToAdd;
            contactServiceSpy.createIfNeededAndAddToSplit.and.returnValue({ then: function (callback) { return callback(newContactReturnedInCallback); } });
            $scope.splitContacts = [];

            $scope.clickOnAddContactByEmail();
        });

        it('should call createIfNeededAndAddToSplit on the contact service', function () {
            expect(contactServiceSpy.createIfNeededAndAddToSplit).toHaveBeenCalledWith(splitIdInState, contactEmailToAdd, undefined);
        });
        it('should fetch and set the split again', function () {
            expect($scope.splitContacts.length).toBeGreaterThan(1);
        });
        it('should empty the textbox', function () {
            expect($scope.form.contactEmailToAdd).toEqual('');
        });
    });

    describe('click on settle', function () {
        beforeEach(function () {
            $scope.goToSettle();
        });
        it('should call navigate to the settle view with the splitId', function () {
            expect($state.go).toHaveBeenCalledWith('settleView', { splitId: splitIdInState });
        });
    });

    describe('change the value of the contact to add', function () {
        beforeEach(function () {
            var phoneContactsReturnedInCallback = [{ displayName: "Olivier Roger" }, { displayName: "Olivier Desvachez"}];
            contactServiceSpy.searchPhoneContacts.and.returnValue({ then: function (callback) { return callback(phoneContactsReturnedInCallback); } });
            $scope.form.contactEmailToAdd = "olivier";
            $scope.searchPhoneContacts();
        });
        it('should call the service with email to search', function () {
            expect(contactServiceSpy.searchPhoneContacts).toHaveBeenCalledWith($scope.form.contactEmailToAdd);
        });
        it('should set the returned contacts in the autocomplete list', function () {
            expect($scope.autocompleteContacts.length).toBeGreaterThan(1);
        });
    });

    describe('change the value of the contact to add to less than 2 chars', function () {
        beforeEach(function () {
            $scope.form.contactEmailToAdd = "o";
            $scope.searchPhoneContacts();
        });
        it('should not call the service', function () {
            expect(contactServiceSpy.searchPhoneContacts).not.toHaveBeenCalled();
        });
        it('should empty the autocomplete list', function () {
            expect($scope.autocompleteContacts.length).toBe(0);
        });
    });

    describe('click on an autocomplete contact', function () {
        var newContactReturnedInCallback = { id: 3, email: 'olivier.roger@gmail.com', name: 'Olivier Roger' }
        beforeEach(function () {
            contactServiceSpy.createIfNeededAndAddToSplit.and.returnValue({ then: function (callback) { return callback(newContactReturnedInCallback); } });
            $scope.splitContacts = [];
            $scope.form.contactEmailToAdd = "oliv";

            $scope.autocompleteContacts = [{ displayName: 'Olivier Roger', emails: [{ value: 'olivier.roger@gmail.com' }] }, { displayName: 'Olivier Desvachez', emails: [{ value: 'olivier.desvachez@gmail.com' }] }];
            $scope.clickOnAutocompleteContact($scope.autocompleteContacts[0]);
        });
        it('should call createIfNeededAndAddToSplit on the contact service', function () {
            expect(contactServiceSpy.createIfNeededAndAddToSplit).toHaveBeenCalledWith(splitIdInState, 'olivier.roger@gmail.com', 'Olivier Roger');
        });
        it('should fetch and set the split again', function () {
            expect($scope.splitContacts.length).toBeGreaterThan(1);
        });
        it('should empty the textbox', function () {
            expect($scope.form.contactEmailToAdd).toEqual('');
        });
        it('should empty the autocomplete list', function () {
            expect($scope.autocompleteContacts.length).toEqual(0);
        });
    });
});