var splitService;
var splitRepositorySpy;
var contactRepositorySpy;
var $httpBackend;

describe('SplitService', function () {
    beforeEach(function () {
        module('PayMeBack');

        //splitRepositorySpy = {
        //    list: jasmine.createSpy(),
        //    get: jasmine.createSpy(),
        //    insert: jasmine.createSpy(),
        //    saveToStorage: jasmine.createSpy(),
        //};
        //contactRepositorySpy = {
        //    get: jasmine.createSpy().and.returnValue(null),
        //    insert: jasmine.createSpy(),
        //    saveToStorage: jasmine.createSpy(),
        //};
        //module(function ($provide) {
        //    $provide.value('splitRepository', splitRepositorySpy);
        //    $provide.value('contactRepository', contactRepositorySpy);
        //    $provide.value('dateTimeProvider', { now: function () { return new Date('29 Dec 2015 15:40:55'); } });
        //});

        inject(function ($injector) {
            splitService = $injector.get('splitService');
            $httpBackend = $injector.get('$httpBackend');
            $httpBackend.when('GET', /^views\//).respond(null); // For some reason a call to a view is in the queue of the $httpBackend... this is a temporary workaround
        });
    });

    describe('list', function () {
        it('should return a list of splits', function () {
            $httpBackend.when('GET', '/splits').respond([{ "id": 1, "name": "Sat 25 Dec 2015 14:48", "date": "2015-12-30T16:34:41.433Z" }, { "id": 2, "name": "Sun 26 Dec 2015 10:18", "date": "2015-12-30T16:34:41.433Z" }]);

            var splits = splitService.list();
            $httpBackend.flush();

            expect(splits.length).toBeGreaterThan(1);
        });
    });

    describe('get', function () {
        it('should return a split with the provided id', function () {
            $httpBackend.when('GET', '/splits/2').respond({ "id": 2, "name": "Sun 26 Dec 2015 10:18", "date": "2015-12-30T16:34:41.433Z" });

            var split = splitService.get(2);
            $httpBackend.flush();

            expect(split.id).toEqual(2);
        });
    });

    //describe('create', function () {
    //    var expectedSplitName = 'Tue Dec 29 2015 15:40';
    //    var splits;
    //    beforeEach(function () {
    //        splits = splitService.create();
    //    });

    //    it('should call the repository with name ' + expectedSplitName, function () {
    //        expect(splitRepositorySpy.insert).toHaveBeenCalledWith(expectedSplitName);
    //    });

    //    it('should call saveToStorage on the repository', function () {
    //        expect(splitRepositorySpy.saveToStorage).toHaveBeenCalled();
    //    });
    //});

    //describe('addContactToSplit with email', function () {
    //    var contactEmail = 'john@user.com';
    //    var split;
    //    beforeEach(function () {
    //        split = new Split(1, 'My Split 1');
    //        splitService.addContactToSplit(split, contactEmail);
    //    });

    //    it('should call the repository to check if user exists', function () {
    //        expect(contactRepositorySpy.get).toHaveBeenCalledWith({ email: contactEmail });
    //    });

    //    describe('when the contact doesn\'t already exist', function () {
    //        it('should call the repository to create a new contact', function () {
    //            expect(contactRepositorySpy.insert).toHaveBeenCalled();
    //        });
    //    });

    //    it('should add a new contact to the split', function () {
    //        expect(split.contactIds.length).toEqual(1);
    //    });

    //    it('should call the repository to save splits and contacts to storage', function () {
    //        expect(splitRepositorySpy.saveToStorage).toHaveBeenCalled();
    //        expect(contactRepositorySpy.saveToStorage).toHaveBeenCalled();
    //    });
    //});
});