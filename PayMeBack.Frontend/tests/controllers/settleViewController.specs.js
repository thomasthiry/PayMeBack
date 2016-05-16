describe('SettleViewController', function () {
    var $controller, settleViewController, splitServiceSpy, $scope = {};
    var settlementsReturnedInCallBack = { transfers: [{ fromContactId: 1, fromContactName: 'Olivier', toContactId: 2, toContactName: 'Thomas', amount: 55.34 }, {fromContactId:3, fromContactName: 'John', toContactId:2, toContactName: 'Thomas', amount: 18 }] };
    //var splitContactIdInState = 2;
    var splitIdInState = 5;

    beforeEach(function () {
        module('PayMeBack');

        splitServiceSpy = jasmine.createSpyObj('splitServiceSpy', ['getSettlement']);
        splitServiceSpy.getSettlement.and.returnValue({ then: function (callback) { return callback(settlementsReturnedInCallBack); } });

        module(function ($provide) {
            $provide.value('splitService', splitServiceSpy);
            $provide.value('$stateParams', { splitId: splitIdInState });
        });

        inject(function ($injector, _$controller_) {
            $controller = _$controller_;
        });

        settleViewController = $controller('SettleViewController', { $scope: $scope });
    });

    describe('controller initialization', function () {
        it('should fetch and set the settlement', function () {
            expect(splitServiceSpy.getSettlement).toHaveBeenCalledWith(splitIdInState);
            expect($scope.settlement.transfers.length).toBeGreaterThan(1);
        });
    });
});