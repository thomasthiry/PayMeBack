var splitService;
var splitRepositorySpy;

beforeEach(function () {
    module('PayMeBack');

    splitRepositorySpy = {
        list: jasmine.createSpy(),
        get: jasmine.createSpy(),
        insert: jasmine.createSpy()
    };
    module(function ($provide) {
        $provide.value('splitRepository', splitRepositorySpy);
    });
    module(function ($provide) {
        $provide.value('dateTimeProvider', { now: function () { return new Date('29 Dec 2015 15:40:55');} });
    });

    inject(function ($injector) {
        splitService = $injector.get('splitService');
    });
});

describe('splitService.list', function () {
    it('should call the repository', function () {
        var splits = splitService.list();
        expect(splitRepositorySpy.list).toHaveBeenCalled();
    });
});

describe('splitService.get', function () {
    it('should call the repository', function () {
        var splits = splitService.get();
        expect(splitRepositorySpy.get).toHaveBeenCalled();
    });
});

describe('splitService.create', function () {
    var expectedSplitName = 'Tue Dec 29 2015 15:40';
    it('should call the repository with name ' + expectedSplitName, function () {
        var splits = splitService.create();
        expect(splitRepositorySpy.insert).toHaveBeenCalledWith(expectedSplitName);
    });
});