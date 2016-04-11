describe('SplitListController', function () {
    var $controller, splitListController, $scope = {}, $state;
    var splitsReturnedInCallback = [{ id: 1, name: 'Today' }, { id: 2, name: 'Tomorrow' }];
    var splitReturnedInCallback = { id: 3, name: 'Now' };

    beforeEach(function () {
        module('PayMeBack');

        splitServiceSpy = jasmine.createSpyObj('splitServiceSpy', ['list', 'create']);
        splitServiceSpy.list.and.returnValue({ then: function (callback) { return callback(splitsReturnedInCallback); } });

        $state = jasmine.createSpyObj('$state', ['go']);
        
        module(function ($provide) {
            $provide.value('splitService', splitServiceSpy);
        });

        inject(function ($injector, _$controller_) {
            $controller = _$controller_;
        });

        splitListController = $controller('SplitListController', { $scope: $scope, $state: $state });
    });

    describe('controller initialization', function () {
        it('should fetch the split list', function () {
            expect(splitServiceSpy.list).toHaveBeenCalled();
            expect($scope.splits.length).toEqual(2);
            expect($scope.splits[1].name).toEqual('Tomorrow');
        });
    });

    describe('click on add', function () {
        beforeEach(function () {

            splitServiceSpy.create.and.returnValue({ then: function (callback) { return callback(splitReturnedInCallback); } });
            $scope.add_click();
        });
        it('should call the split service', function () {
            expect(splitServiceSpy.create).toHaveBeenCalled();
        });
        it('should call navigate to the view of the created split', function () {
            expect($state.go).toHaveBeenCalledWith('splitView', { splitId: splitReturnedInCallback.id });
        });
    });
});