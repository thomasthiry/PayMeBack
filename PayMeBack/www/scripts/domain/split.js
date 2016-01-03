function Split(id, name) {
    this.id = id
    this.name = name;
    this.date = new Date();
    this.contactIds = [];

    this.addContact = function (contactId) {
        this.contactIds.push(contactId);
    }
}