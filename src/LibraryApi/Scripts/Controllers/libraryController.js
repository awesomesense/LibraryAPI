(function () {
    'use strict';

    angular
        .module('libraryApp')
        .controller('libraryController', libraryController);

    libraryController.$inject = ['$scope']; 

    function libraryController($scope) {
        $scope.title = 'libraryController';

        activate();

        function activate() { }
    }
})();
