/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tedushop.slides', ['tedushop.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('slides', {
                url: "/slides",
                parent:'base',
                templateUrl: "/app/components/slides/slideListView.html",
                controller: "slideListController"
            })
            .state('slide_add', {
                url: "/add_slide",
                parent: 'base',
                templateUrl: "/app/components/slides/slideAddView.html",
                controller: "slideAddController"
            })
            .state('slide_edit', {
                url: "/edit_slide/:id",
                parent: 'base',
                templateUrl: "/app/components/slides/slideEditView.html",
                controller: "slideEditController"
            });
    }
})();