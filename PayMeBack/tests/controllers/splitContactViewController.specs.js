describe('SplitContactViewController', function () {
    var $controller, splitContactViewController, contactServiceSpy, $scope = {};
    var splitContactReturnedInCallback = { id: 2, splitId: 3, contactId: 5, name: 'Olivier', email: 'olivier@gmail.com' };
    var splitContactIdInState = 2;
    var splitIdInState = 5;

    beforeEach(function () {
        module('PayMeBack');

        contactServiceSpy = jasmine.createSpyObj('contactServiceSpy', ['getSplitContactById', 'updateSplitContact']);
        contactServiceSpy.getSplitContactById.and.returnValue({ then: function (callback) { return callback(splitContactReturnedInCallback); } });
        contactServiceSpy.updateSplitContact.and.returnValue({ then: function (callback) { return callback(); } });

        module(function ($provide) {
            $provide.value('contactService', contactServiceSpy);
            $provide.value('$stateParams', { splitId: splitIdInState, splitContactId: splitContactIdInState });
        });

        inject(function ($injector, _$controller_) {
            $controller = _$controller_;
        });

        splitViewController = $controller('SplitContactViewController', { $scope: $scope });
    });

    describe('controller initialization', function () {
        it('should fetch and set the splitContact and form details', function () {
            expect(contactServiceSpy.getSplitContactById).toHaveBeenCalledWith(splitIdInState, splitContactIdInState);
            expect($scope.splitContact.name).toEqual('Olivier');
        });
    });

    describe('click on save', function () {
        beforeEach(function () {
            $scope.splitContact = { id: splitContactIdInState, splitId: 5, owes: 111, paid: 222, comments: 'much more' };
            $scope.save_click();
        });
        it('should call the contact service', function () {
            expect(contactServiceSpy.updateSplitContact).toHaveBeenCalledWith($scope.splitContact.splitId, $scope.splitContact.id, $scope.splitContact.owes, $scope.splitContact.paid, $scope.splitContact.comments);
        });
    });
});