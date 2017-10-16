(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            if (input == true)
                return 'kích hoạt';
            else
                return 'Khóa';
        }
    });
})(angular.module('tedushop.common'));