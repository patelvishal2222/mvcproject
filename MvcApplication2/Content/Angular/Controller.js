
app.controller("myCntrl", function ($scope, myService) {
    
    $scope.divEmployee = false;
    
    GetAllEmployee();
    
     
    //To Get All Records 
    function GetAllEmployee() {
        debugger;
        var getData = myService.getEmployees();
        debugger;
        getData.then(function (response) {
            $scope.Empes = response.data;
        }, function (response) {
            debugger;;
            alert('Error in getting records');
        });
    }

    $scope.editEmployee = function (Emp) {
        
        //var getData = myService.getEmployee(Emp.EmpId);
        
        //getData.then(function (emp) {
            $scope.Emp = Emp;
            $scope.EmpId = Emp.EmpId;
            $scope.EmpName = Emp.EmpName;
            $scope.Amount = Emp.Amount;
            $scope.ImagePath = Emp.ImagePath;
            $scope.Action = "Update";
            $scope.divEmployee = true;
        
    }

    $scope.AddUpdateEmployee = function () {
        debugger;
        var Emp = {
            EmpName: $scope.EmpName,
            Amount: $scope.Amount,
            ImagePath: $scope.ImagePath
        };
        var getAction = $scope.Action;

        if (getAction == "Update") {
            Emp.EmpId = $scope.EmpId;
            var getData = myService.AddUpdateEmp(Emp);
            getData.then(function (msg) {
                GetAllEmployee();
                alert(msg.data);
                $scope.divEmployee = false;
            }, function () {
                alert('Error in updating record');
            });
        } else {
            var getData = myService.AddUpdateEmp(Emp);
            getData.then(function (msg) {
                GetAllEmployee();
                alert(msg.data);
                $scope.divEmployee = false;
            }, function () {
                alert('Error in adding record');
            });
        }
    }

    $scope.AddEmployeeDiv = function () {
        ClearFields();
        $scope.Action = "Add";
        //$scope.divEmployee = true;
        
        
        $scope.divEmployee = true;

        dialog = $("#divEmployee").dialog({
            autoOpen: true,
            height: 400,
            width: 350,
            modal: true
            
        });
        alert(2);
        
    }

    $scope.deleteEmployee = function (Emp) {
        var strconfirm = confirm("Are you sure you want to delete?");
        if (strconfirm == true) {
            var getData = myService.DeleteEmp(Emp.EmpId);
            getData.then(function (msg) {
                GetAllEmployee();
                alert('Employee Deleted');
            }, function () {
                alert('Error in Deleting Record');
            });
        }
    }

    function ClearFields() {
        $scope.EmpId = "";
        $scope.EmpName = "";
        $scope.Amount = "";
        $scope.ImagePath = "";
    }
});

