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

    describe('list', function () {
        var splitContactJohn, splitContactMark, splitContactSam;
        beforeEach(function () {
            splitContactJohn = new SplitContact(0, 'john@user.com');
            splitContactJohn.id = contactRepository.insert(splitContactJohn);
            splitContactMark = new SplitContact(0, 'mark@user.com');
            splitContactMark.id = contactRepository.insert(splitContactMark);
            splitContactSam = new SplitContact(0, 'sam@user.com');
            splitContactSam.id = contactRepository.insert(splitContactSam);
        });
        it('should return all the contacts when no query provided', function () {
            var fetchedContacts = contactRepository.list();
            expect(fetchedContacts.length).toEqual(3);
        });
        it('should return all the contacts when query all ids', function () {
            var fetchedContacts = contactRepository.list({ ids: [splitContactJohn.id, splitContactMark.id, splitContactSam.id] });
            expect(fetchedContacts.length).toEqual(3);
        });
        it('should return only this contact when query only 1 id', function () {
            var fetchedContacts = contactRepository.list({ ids: [splitContactMark.id] });
            expect(fetchedContacts.length).toEqual(1);
            expect(fetchedContacts[0].email).toEqual(splitContactMark.email);
        });
        it('should return an empty array when no id match', function () {
            var fetchedContacts = contactRepository.list({ ids: [999999] });
            expect(fetchedContacts.length).toEqual(0);
        });
        it('should return the matched contacts when there is also unexisting ids', function () {
            var fetchedContacts = contactRepository.list({ ids: [888888, splitContactMark.id, 999999] });
            expect(fetchedContacts.length).toEqual(1);
        });
    });

    describe('loadFromStorage', function () {
        var initialContacts, finalContacts;
        beforeEach(function () {
            initialContacts = contactRepository.list();

            spyOn(localStorage, 'getItem').and.returnValue('[{"id":1,"email":"john@user.com"},{"id":2,"email":"mark@user.com"}]');
            contactRepository.loadFromStorage();

            finalContacts = contactRepository.list();
        });

        it('should call local storage', function () {
            expect(localStorage.getItem).toHaveBeenCalledWith('contacts');
        });

        it('should contain more contacts than before', function () {
            expect(initialContacts.length).toEqual(0);
            expect(finalContacts.length).toEqual(2);
        });

        it('should contain actual Contact objects, not just objects with similar fields', function () {
            expect(finalContacts[0] instanceof SplitContact).toBeTruthy();
        });
    });

    describe('saveToStorage', function () {
        beforeEach(function () {
            spyOn(localStorage, 'setItem');
            contactRepository.insert(new SplitContact(0, 'john@contact.com'));
            contactRepository.saveToStorage();
        });

        it('should call local storage', function () {
            expect(localStorage.setItem).toHaveBeenCalled();
        });
    });
});