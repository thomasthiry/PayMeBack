var splitService;
var splitRepositorySpy;

describe('SplitService', function () {
    beforeEach(function () {
        module('PayMeBack');

        splitRepositorySpy = {
            list: jasmine.createSpy(),
            get: jasmine.createSpy(),
            insert: jasmine.createSpy(),
            saveToStorage: jasmine.createSpy()
        };
        module(function ($provide) {
            $provide.value('splitRepository', splitRepositorySpy);
        });
        module(function ($provide) {
            $provide.value('dateTimeProvider', { now: function () { return new Date('29 Dec 2015 15:40:55'); } });
        });

        inject(function ($injector) {
            splitService = $injector.get('splitService');
        });
    });

    describe('list', function () {
        it('should call the repository', function () {
            var splits = splitService.list();
            expect(splitRepositorySpy.list).toHaveBeenCalled();
        });
    });
    // todo: pass Id
    describe('get', function () {
        it('should call the repository', function () {
            var splits = splitService.get();
            expect(splitRepositorySpy.get).toHaveBeenCalled();
        });
    });

    describe('create', function () {
        var expectedSplitName = 'Tue Dec 29 2015 15:40';
        var splits;
        beforeEach(function () {
            splits = splitService.create();
        });

        it('should call the repository with name ' + expectedSplitName, function () {
            expect(splitRepositorySpy.insert).toHaveBeenCalledWith(expectedSplitName);
        });

        it('should call saveToStorage on the repository', function () {
            expect(splitRepositorySpy.saveToStorage).toHaveBeenCalled();
        });
    });
});