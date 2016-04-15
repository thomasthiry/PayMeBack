var splitService, splitRepositorySpy, contactRepositorySpy, $httpBackend, backendHostUrl;
var nowText = "Tue Dec 29 2015 15:40";
var nowDate = "2015-12-29T15:40:00.000Z";

describe('SplitService', function () {
    beforeEach(function () {
        module('PayMeBack');
        module(function ($provide) {
            $provide.value('dateTimeProvider', { now: function () { return new Date(nowText); } });
        });
        inject(function ($injector) {
            splitService = $injector.get('splitService');
            $httpBackend = $injector.get('$httpBackend');
            backendHostUrl = $injector.get('backendHostUrl');
            $httpBackend.when('GET', /^views\//).respond(null); // For some reason a call to a view is in the queue of the $httpBackend... this is a temporary workaround
        });
    });

    describe('list', function () {
        it('should return a list of splits', function () {
            $httpBackend.when('GET', backendHostUrl + '/splits').respond([{ "id": 1, "name": "Sat 25 Dec 2015 14:48", "date": "2015-12-30T16:34:41.433Z" }, { "id": 2, "name": "Sun 26 Dec 2015 10:18", "date": "2015-12-30T16:34:41.433Z" }]);
            var _splits;
            splitService.list().then(function (splits) {
                _splits = splits;
            });
            $httpBackend.flush();

            expect(_splits[1].id).toEqual(2);
        });
    });

    describe('get', function () {
        it('should return a split with the provided id', function () {
            $httpBackend.when('GET', backendHostUrl + '/splits/2').respond({ "id": 2, "name": "Sun 26 Dec 2015 10:18", "date": "2015-12-30T16:34:41.433Z" });

            var _split;
            splitService.get(2).then(function (split) {
                _split = split;
            });
            $httpBackend.flush();

            expect(_split.id).toEqual(2);
        });
    });

    describe('create', function () {
        var expectedSplitName = nowText;
        var returnedSplit;
        beforeEach(function () {
            $httpBackend.when('POST', backendHostUrl + '/splits').respond({ "id": 2, "name": nowText, "date": nowDate });
            splitService.create().then(function (split) {
                returnedSplit = split;
            });
            $httpBackend.flush();
        });

        it('should return the created split with the provided id', function () {
            expect(returnedSplit.id).toEqual(2);
        });

        it('the name of the split should be: ' + expectedSplitName, function () {
            expect(returnedSplit.name).toEqual(expectedSplitName);
        });
    });

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