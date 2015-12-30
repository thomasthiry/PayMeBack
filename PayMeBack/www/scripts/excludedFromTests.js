angular.module('PayMeBack').run(function (splitRepository) {
    splitRepository.loadFromStorage();
})