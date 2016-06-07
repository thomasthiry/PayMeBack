var contactService, contactRepositorySpy, $cordovaContactsSpy;

describe('ContactService', function () {
    beforeEach(function () {
        module('PayMeBack');

        $cordovaContactsSpy = jasmine.createSpyObj('$cordovaContactsSpy', ['find']);

        module(function ($provide) {
            $provide.value('$cordovaContacts', $cordovaContactsSpy);
        });

        inject(function ($injector) {
            contactService = $injector.get('contactService');
            $httpBackend = $injector.get('$httpBackend');
            backendHostUrl = $injector.get('backendHostUrl');
            $httpBackend.when('GET', /^views\//).respond(null); // For some reason a call to a view is in the queue of the $httpBackend... this is a 'temporary' workaround
        });
    });

    describe('getBySplitId', function () {
        it('should return the list of contact splits', function () {
            $httpBackend.when('GET', backendHostUrl + '/splits/1/contacts').respond([{ id: 1, email: 'Olivier' }, { id: 2, email: 'John' }]);
            var _contacts;
            contactService.getBySplitId(1).then(function (contacts) {
                _contacts = contacts;
            });
            $httpBackend.flush();

            expect(_contacts[0].email).toEqual('Olivier');
        });
    });

    describe('createIfNeededAndAddToSplit', function () {
        it('should call the web service', function () {
            var expectedContact = { email: 'john@facebook.com', splitId: 1 };
            $httpBackend.expect('POST', backendHostUrl + '/splits/1/contacts', expectedContact).respond(201, '');
            contactService.createIfNeededAndAddToSplit(expectedContact.splitId, expectedContact.email);
            $httpBackend.flush();
        });
    });

    describe('getSplitContactById', function () {
        it('should call the web service', function () {
            var expectedSplitContact = { id: 2, splitId: 3, contactId: 5, name: 'Olivier', email: 'olivier@gmail.com' };
            $httpBackend.expect('GET', backendHostUrl + '/splits/3/contacts/2').respond({ id: 2, splitId: 3, contactId: 5, name: 'Olivier', email: 'olivier@gmail.com' });
            contactService.getSplitContactById(expectedSplitContact.splitId, expectedSplitContact.id);
            $httpBackend.flush();
        });
    });

    describe('updateSplitContact', function () {
        it('should call the web service', function () {
            var splitContactToUpdate = { id: 2, splitId: 3, contactId: 5, name: 'Olivier', email: 'olivier@gmail.com', owes: 111, paid: 222, comments: 'more' };
            $httpBackend.expect('PUT', backendHostUrl + '/splits/3/contacts/2').respond(200, '');
            contactService.updateSplitContact(splitContactToUpdate);
            $httpBackend.flush();
        });
    });

    describe('searchPhoneContacts', function () {
        it('should call the $cordovaContacts service', function () {
            var phoneContactsReturnedInCallback = [{ displayName: "Olivier Roger" }, { displayName: "Olivier Desvachez" }];
            $cordovaContactsSpy.find.and.returnValue({ then: function (callback) { return callback(phoneContactsReturnedInCallback); } });
            contactService.searchPhoneContacts('olivier');
            expect($cordovaContactsSpy.find).toHaveBeenCalled();
        });
    });
});