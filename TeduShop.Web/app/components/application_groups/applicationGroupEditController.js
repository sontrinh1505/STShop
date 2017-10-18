(function (app) {
    'use strict';

    app.controller('applicationGroupEditController', applicationGroupEditController);

    applicationGroupEditController.$inject = ['$scope', 'apiService', 'notificationService', '$location', '$stateParams'];

    function applicationGroupEditController($scope, apiService, notificationService, $location, $stateParams) {

        $scope.group = {}


        $scope.updateApplicationGroup = updateApplicationGroup;

        function updateApplicationGroup() {
            apiService.put('/api/applicationGroup/update', $scope.group, addSuccessed, addFailed);
        }

        function loadDetail(callbackDetail) {
            apiService.get('/api/applicationGroup/detail/' + $stateParams.id, null,
            function (result) {
                $scope.group = result.data;
                
                var rolePermissions = result.data.Permissions;
                callbackDetail(rolePermissions);
            },
            function (result) {
                notificationService.displayError(result.data);
            });
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.group.Name + ' đã được cập nhật thành công.');

            $location.url('application_groups');
        }

        function addFailed(response) {
            notificationService.displayError(response.data.Message);
            notificationService.displayErrorValidation(response);
        }

       
        function loadRoles(callback) {
            apiService.get('/api/applicationRole/getlistall',
                null,
                function (response) {
                    //$scope.roles = response.data;
                    var roles = response.data;

                    callback(roles);

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

                    loadRoles(function (_roles) {
                        permissions.forEach(function (permission, index) {
                            permission.allRole = _roles;
                        });
                        $scope.group.Permissions = permissions;
                    });

                    loadDetail(function (_rolePermissions) {
                        permissions.forEach(function (permission, index) {
                            _rolePermissions.forEach(function (rolePermission, index) {
                                if (rolePermission.ID == permission.ID)
                                {
                                    permission.Roles = rolePermission.Roles;
                                }
                            });
                                                  
                        });
                        $scope.group.Permissions = permissions;
                   });
                    $scope.group.Permissions = permissions;

                }, function (response) {
                    notificationService.displayError('can not load permissions.');
                });

        }
        

        loadPermissions()
        //loadRoles();
        //loadDetail();
    }
})(angular.module('tedushop.application_groups'));