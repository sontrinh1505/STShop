/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tedushop.systemconfigs', ['tedushop.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('systemconfigs', {
                url: "/systemconfigs",
                parent:'base',
                templateUrl: "/app/components/systemconfigs/systemconfigListView.html",
                controller: "systemconfigListController"
            })
            .state('systemconfig_add', {
                url: "/add_systemconfig",
                parent: 'base',
                templateUrl: "/app/components/systemconfigs/systemconfigAddView.html",
                controller: "systemconfigAddController"
            })
            .state('systemconfig_edit', {
                url: "/edit_systemconfig/:id",
                parent: 'base',
                templateUrl: "/app/components/systemconfigs/systemconfigEditView.html",
                controller: "systemconfigEditController"
            });
    }
})();