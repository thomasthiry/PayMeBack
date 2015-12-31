var contactRepository;

describe('ContactRepository', function () {
    beforeEach(function () {
        module('PayMeBack');

        inject(function ($injector) {
            contactRepository = $injector.get('contactRepository');
        });
    });

    describe('insert first contact', function () {
        var splitContact = new SplitContact(0, 'john@contact.com');
        var insertedContactId;
        beforeEach(function () {
            insertedContactId = contactRepository.insert(splitContact);
        });

        it('should return a contact Id of 1', function () {
            expect(insertedContactId).toEqual(1);
        });
    });

    describe('get one contact based on email', function () {
        it('should return the contact', function () {
            var splitContactJohn = new SplitContact(0, 'john@user.com');
            splitContactJohn.id = contactRepository.insert(splitContactJohn);
            var splitContactMark = new SplitContact(0, 'mark@user.com');
            splitContactMark.id = contactRepository.insert(splitContactMark);
            
            var fetchedContact = contactRepository.get({ email: splitContactMark.email });

            expect(fetchedContact.email).toEqual(splitContactMark.email);
        });
    });
});