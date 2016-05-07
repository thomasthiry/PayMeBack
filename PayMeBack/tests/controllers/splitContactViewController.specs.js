describe('SplitContactViewController', function () {
    var $controller, splitContactViewController, contactServiceSpy, $scope = {};
    var splitContactReturnedInCallback = { id: 2, splitId: 3, contactId: 5, name: 'Olivier', email: 'olivier@gmail.com' };
    var splitContactIdInState = 2;

    beforeEach(function () {
        module('PayMeBack');

        contactServiceSpy = jasmine.createSpyObj('contactServiceSpy', ['getSplitContactById']);
        contactServiceSpy.getSplitContactById.and.returnValue({ then: function (callback) { return callback(splitContactReturnedInCallback); } });

        module(function ($provide) {
            $provide.value('contactService', contactServiceSpy);
            $provide.value('$stateParams', { splitContactId: splitContactIdInState });
        });

        inject(function ($injector, _$controller_) {
            $controller = _$controller_;
        });

        splitViewController = $controller('SplitContactViewController', { $scope: $scope });
    });

    describe('controller initialization', function () {
        it('should fetch and set the splitContact', function () {
            expect(contactServiceSpy.getSplitContactById).toHaveBeenCalledWith(splitContactIdInState);
            expect($scope.splitContact.name).toEqual('Olivier');
        });
    });
});