/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tedushop.menu_groups', ['tedushop.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('menu_groups', {
                url: "/menu_groups",
                parent:'base',
                templateUrl: "/app/components/menu_groups/menuGroupListView.html",
                controller: "menuGroupListController"
            })
            .state('menu_groups_add', {
                url: "/add_menu_groups",
                parent: 'base',
                templateUrl: "/app/components/menu_groups/menuGroupAddView.html",
                controller: "menuGroupAddController"
            })
            .state('menu_groups_edit', {
                url: "/edit_menu_groups/:id",
                parent: 'base',
                templateUrl: "/app/components/menu_groups/menuGroupEditView.html",
                controller: "menuGroupEditController"
            });
    }
})();