function Split(id, name) {
    this.id = id
    this.name = name;
    this.date = new Date();
    this.contacts = [];

    this.addContact = function (splitContact) {
        this.contacts.push(splitContact);
    }
}