(function (app) {
    'use strict';

    app.controller('applicationGroupAddController', applicationGroupAddController);

    applicationGroupAddController.$inject = ['$scope', 'apiService', 'notificationService', '$location', 'commonService'];

    function applicationGroupAddController($scope, apiService, notificationService, $location, commonService) {
        $scope.group = {
            ID: 0,
            // Roles: [],
            Permissions: {
                Roles: {}
            }
        }

        $scope.addAppGroup = addApplicationGroup;

        function addApplicationGroup() {
            apiService.post('/api/applicationGroup/add', $scope.group, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.group.Name + ' đã được thêm mới.');

            $location.url('application_groups');
        }

        function addFailed(response) {
            notificationService.displayError(response.data.Message);
            notificationService.displayErrorValidation(response);
        }

        function loadRoles(calback) {
            apiService.get('/api/applicationRole/getlistall',
                null,
                function (response) {
                    //$scope.roles = response.data;
                    var roles = response.data;

                    calback(roles);

                }, function (response) {
                    notificationService.displayError('can not load roles.');
                    return null;
                });
        }

        var permissonRole = [];
        
        function loadPermissions() {
            apiService.get('/api/applicationGroup/getlistpermission',
                null,
                function (response) {
                    var permissions = response.data;
                    var _pers = [];
                    loadRoles(function (_roles) {
                        permissions.forEach(function (permission, index) {
                            var item = new Array();
                            _roles.forEach(function (role, index) {

                                var itemob = { index: "0", ID: "0", Name: "", Description: "" };
                                itemob.index = permission.ID;
                                itemob.ID = role.ID;
                                itemob.Name = role.Name;
                                itemob.Description = role.Description;

                                item.push(itemob);
                            });
                            permission.Roles = item;
                            permissonRole.push(permission);
                        });
                        $scope.permissions = permissonRole;
                    });


                }, function (response) {
                    notificationService.displayError('can not load permissions.');
                });

        }
        //loadRoles();
        loadPermissions();

    }
})(angular.module('tedushop.application_groups'));