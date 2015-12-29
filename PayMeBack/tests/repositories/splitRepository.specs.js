﻿var splitRepository;

describe('SplitRepository', function () {
    beforeEach(function () {
        module('PayMeBack');

        inject(function ($injector) {
            splitRepository = $injector.get('splitRepository');
        });
    });

    describe('insert first split', function () {
        var splitName = 'mysplit1';
        var insertedSplit;
        beforeEach(function () {
            insertedSplit = splitRepository.insert(splitName);
        });

        it('should return the inserted split', function () {
            expect(insertedSplit.date).toEqual(splitName);
        });

        it('should have an id of 1', function () {
            expect(insertedSplit.id).toEqual(1);
        });
    });

    describe('get one split', function () {
        it('should return the inserted split', function () {
            var splitName = 'mysplit1';
            splitRepository.insert('another split 1');
            var insertedSplit = splitRepository.insert(splitName);
            splitRepository.insert('another split 2');
            var fetchedSplit = splitRepository.get(insertedSplit.id);

            expect(fetchedSplit.id).toEqual(insertedSplit.id);
            expect(fetchedSplit.date).toEqual(splitName);
        });
    });

    describe('list', function () {
        it('should return all splits that were inserted', function () {
            var insertedSplit = splitRepository.insert('mysplit1');
            var insertedSplit2 = splitRepository.insert('myothersplit');

            var splits = splitRepository.list();

            expect(splits.length).toEqual(2);
        });
    });

    describe('loadFromStorage', function () {
        var initialSplits, finalSplits;
        beforeEach(function () {
            initialSplits = splitRepository.list();
            
            spyOn(localStorage, 'getItem').and.returnValue('[{ "id": 1, "date": "Sat 25 Dec 14:48" }, { "id": 2, "date": "Sun 26 Dec 10:18" }]');;
            splitRepository.loadFromStorage();

            finalSplits = splitRepository.list();
        });

        it('should call local storage', function () {
            expect(localStorage.getItem).toHaveBeenCalledWith('splits');
        });
        it('should contain more splits than before', function () {
            expect(initialSplits.length).toEqual(0);
            expect(finalSplits.length).toEqual(2);

        });
    });

    describe('saveToStorage', function () {
        beforeEach(function () {
            spyOn(localStorage, 'setItem');
            splitRepository.insert('Sat 25 Dec 14:48');
            splitRepository.saveToStorage();
        });

        it('should call local storage', function () {
            expect(localStorage.setItem).toHaveBeenCalledWith('splits', '[{"date":"Sat 25 Dec 14:48","id":1}]');
        });
    });
});