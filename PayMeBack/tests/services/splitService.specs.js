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
            $httpBackend.when('GET', /^views\//).respond(null); // For some reason a call to a view is in the queue of the $httpBackend... this is a 'temporary' workaround
        });
    });

    describe('list', function () {
        it('should return a list of splits', function () {
            $httpBackend.when('GET', backendHostUrl + '/splits').respond([{ "id": 1, "name": "Sat 25 Dec 2015 14:48", "created": "2015-12-30T16:34:41.433Z" }, { "id": 2, "name": "Sun 26 Dec 2015 10:18", "created": "2015-12-30T16:34:41.433Z" }]);
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
            $httpBackend.when('GET', backendHostUrl + '/splits/2').respond({ "id": 2, "name": "Sun 26 Dec 2015 10:18", "created": "2015-12-30T16:34:41.433Z" });

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
            $httpBackend.when('POST', backendHostUrl + '/splits').respond({ "id": 2, "name": nowText, "created": nowDate });
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

    describe('getSettlement', function () {
        it('should call the web service', function () {
            var settlementsReturnedInCallBack = { transfers: [{ fromContactId: 1, fromContactName: 'Olivier', toContactId: 2, toContactName: 'Thomas', amount: 55.34 }, { fromContactId: 3, fromContactName: 'John', toContactId: 2, toContactName: 'Thomas', amount: 18 }] };
            $httpBackend.when('GET', backendHostUrl + '/splits/2/settle').respond(settlementsReturnedInCallBack);

            var _settlement;
            splitService.getSettlement(2).then(function (settlement) {
                _settlement = settlement;
            });
            $httpBackend.flush();

            expect(_settlement.transfers.length).toBeGreaterThan(1);
        });
    });
});