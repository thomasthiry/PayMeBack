describe('Split', function () {
    describe('constructor', function () {
        var splitName = 'mysplit1';
        var splitId = 5;
        var split;
        beforeEach(function () {
            split = new Split(splitId, splitName);
        });

        it('should fill id and name', function () {
            expect(split.id).toEqual(splitId);
            expect(split.name).toEqual(splitName);
        });

        it('should store the current time in date field', function () {
            var OneMinuteAgo = new Date(new Date().getTime() - 60000);
            var InOneMinute = new Date(new Date().getTime() + 60000);

            expect(split.date).toBeGreaterThan(OneMinuteAgo);
            expect(split.date).toBeLessThan(InOneMinute);
        });
    });
});