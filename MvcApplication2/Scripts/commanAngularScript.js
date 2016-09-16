var Account = angular.module('Account', []);


function UrlData($http, Url, Method, Params, Data) {

    //url start
    var response = $http({
        url: Url,
        method: Method,
        params: Params,
        data: Data,
    })
    return response;


}



Account.directive('chintan', function () {


    return function (scope, element, attrs) {
        element.datepicker({
            inline: true,
            format: 'dd-M-yyyy',
            autoclose: true,
            onSelect: function (dateText) {
                var modelPath = $(this).attr('ng-model');
                putObject(modelPath, scope, dateText);
                scope.$apply();
            }
        });
    }
})
Account.filter('dateFilter', function () {
    return function (item) {
        
        if (item != null) {
            var valueData = item.substr(6, item.length - 8);
            return  new Date(parseInt(valueData));
            
            
            
        }
        else if (item.toString() == '/Date(-62135568000000)/') {
            return new "";
        }
        else
            return "";

    };
});
Account.filter('titleCase', function () {
    return function (input) {
        input = input || '';
        return input.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1); });
    };
})



Account.directive('restrictInput', [function () {

    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var ele = element[0];
            var regex = RegExp(attrs.restrictInput);
            var value = ele.value;

            ele.addEventListener('keyup', function (e) {
                if (regex.test(ele.value)) {
                    value = ele.value;
                } else {
                    ele.value = value;
                }
            });
        }
    };
}]);


