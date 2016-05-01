var contactService;
var contactRepositorySpy;

describe('ContactService', function () {
    beforeEach(function () {
        module('PayMeBack');

        inject(function ($injector) {
            contactService = $injector.get('contactService');
            $httpBackend = $injector.get('$httpBackend');
            backendHostUrl = $injector.get('backendHostUrl');
            $httpBackend.when('GET', /^views\//).respond(null); // For some reason a call to a view is in the queue of the $httpBackend... this is a 'temporary' workaround
        });
    });

    describe('getBySplitId', function () {
        it('should return the list of contact splits', function () {
            $httpBackend.when('GET', backendHostUrl + '/splits/1/contacts').respond([{ id: 1, name: 'Olivier' }, { id: 2, name: 'John' }]);
            var _contacts;
            contactService.getBySplitId(1).then(function (contacts) {
                _contacts = contacts;
            });
            $httpBackend.flush();

            expect(_contacts[0].name).toEqual('Olivier');
        });
    });
});