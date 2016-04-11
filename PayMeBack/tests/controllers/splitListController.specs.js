describe('SplitListController', function () {
    var $controller, splitListController, $scope = {};
    var splitsReturnedInCallback = [{ id: 1, name: 'Today' }, { id: 2, name: 'Tomorrow' }];

    beforeEach(function () {
        module('PayMeBack');

        splitServiceSpy = jasmine.createSpyObj('splitServiceSpy', ['list']);
        splitServiceSpy.list.and.returnValue({ then: function (callback) { return callback(splitsReturnedInCallback); } });

        module(function ($provide) {
            $provide.value('splitService', splitServiceSpy);
        });

        inject(function ($injector, _$controller_) {
            $controller = _$controller_;
        });

        splitListController = $controller('SplitListController', { $scope: $scope });
    });

    describe('controller initialization', function () {
        it('should fetch the split list', function () {
            expect(splitServiceSpy.list).toHaveBeenCalled();
            expect($scope.splits.length).toEqual(2);
            expect($scope.splits[1].name).toEqual('Tomorrow');
        });
    });
});