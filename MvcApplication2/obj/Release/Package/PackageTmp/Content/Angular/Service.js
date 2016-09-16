
app.service("myService", function ($http) {

    //get All Eployee
    this.getEmployees = function () {
        var response = $http({
            method: "get",
                    url: "/Emp/GetAll",
            dataType: "json"
                    
                });
            return response;
        //return $http.get("Emp/GetAll");
    }


    // get Employee By Id
    this.getEmployee = function (EmpId) {
        var response = $http({
            method: "post",
            url: "/Emp/GetEmp",
            params: {
                id: JSON.stringify(EmpId)
            }
        });
        return response;
    }

  

    // Add Employee
    this.AddUpdateEmp = function (emp) {
        var response = $http({
            method: "post",
            url: "/Emp/AddUpdateEmp",
            data: JSON.stringify(emp),
            dataType: "json"
        });
        return response;
    }

    //Delete Employee
    this.DeleteEmp = function (EmpId) {
        var response = $http({
            method: "post",
            url: "/Emp/Delete",
            params: {
                Id: JSON.stringify(EmpId)
            }
        });
        return response;
    }

});