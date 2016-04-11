//var contactService;
//var contactRepositorySpy;

//describe('ContactService', function () {
//    beforeEach(function () {
//        module('PayMeBack');

//        contactRepositorySpy = {
//            list: jasmine.createSpy(),
//        };
//        module(function ($provide) {
//            $provide.value('contactRepository', contactRepositorySpy);
//        });

//        inject(function ($injector) {
//            contactService = $injector.get('contactService');
//        });
//    });

//    describe('list', function () {
//        it('should call the repository with the same parameter', function () {
//            var query = { ids: [1, 3] };
//            contactService.list(query);
//            expect(contactRepositorySpy.list).toHaveBeenCalledWith(query);
//        });
//    });
//});