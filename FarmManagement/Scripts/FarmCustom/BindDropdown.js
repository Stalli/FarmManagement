bindYear = function (yearId) {
    $("#" + yearId).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/Attendance/GetYears' } } },
    });
}

bindMonth = function (monthId) {
    $("#" + monthId).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/Attendance/GetMonths' } } },
    });
}

bindEmployeeTypeWithEmployee = function (employeeTypeId, employeeId) {
    $("#" + employeeTypeId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/Attendance/GetEmployeeType' } } },
        change: function (e) {
            bindEmployeeAccordingToType(employeeId, this.value());
        }
    });
}

function bindEmployeeAccordingToType(employeeId, employeeTypeId) {
    if (employeeTypeId == 0) {
        $("#" + employeeId).data("kendoDropDownList").setDataSource([]);
        return;
    }
    $("#" + employeeId).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/Attendance/GetEmployee?employeeType=' + employeeTypeId } } },
    });
}

function bindAllEmployees(id) {
    $("#" + id).kendoDropDownList({
        optionLabel: "Select",
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/Attendance/GetAllEmployees' } } },
    });
}

function bindTaskStatus(taskStatusId) {
    $("#" + taskStatusId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/TaskAssignment/GetTaskStatus' } } },
    });
}

function bindTaskRemarks(taskRemarksId) {
    $("#" + taskRemarksId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/TaskAssignment/GetTaskRemarks' } } },
    });
}


function bindPayByEmployee(personalEmployeeId) {
    $("#" + personalEmployeeId).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/LoanManagement/GetPersonalAccountEmployees' } } },
    });
}

function bindPayByAccountEmployees(payById, employeeId) {
    console.log(employeeId);
    $("#" + payById).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/LoanManagement/GetPersonalPayByEmployees?employeeId=' + employeeId } } },
    });
}

function bindLoanEmployees(loanEmployeeId) {
    $("#" + loanEmployeeId).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/LoanManagement/GetLoanEmployees' } } },
    });
}

function bindLoanEmployeesWithCallback(loanEmployeeId, callback) {
    $("#" + loanEmployeeId).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/LoanManagement/GetLoanEmployees' } } },
        change: callback
    });
}

bindPayByEmployees = function (payById) {
    $("#" + payById).kendoDropDownList({
        optionLabel: "Select",
        dataTextField: "Name",
        dataValueField: "PayByUserId",
        dataSource: { type: "json", transport: { read: { url: '/CommonDb/GetPayByEmployees' } } },
    });
};

bindAnimalCategory = function (animalCategoryId) {
    $("#" + animalCategoryId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/Stock/GetAnimalCategory' } } },
    });
};

bindAnimalGender = function (animalGenderId) {
    $("#" + animalGenderId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/Stock/GetAnimalGender' } } },
    });
};

bindQuality = function (qualityId) {
    $("#" + qualityId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/Stock/GetQuality' } } },
    });
};
bindQuantity = function (qualityId) {
    $("#" + qualityId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/Stock/GetQuantity' } } },
    });
};
bindCondition = function (qualityId) {
    $("#" + qualityId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/Stock/GetCondition' } } },
    });
};
bindFertilizerQuantity = function (quantityId) {
    $("#" + quantityId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/Stock/GetFertilizerQuantity' } } },
    });
};
bindFertilizerType = function (typeId) {
    $("#" + typeId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/Stock/GetFertilizerType' } } },
    });
};

bindVehicleType = function (vehicleId) {
    $("#" + vehicleId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/Stock/GetVehicleType' } } },
    });
};

bindOtherItem = function (otherItemId) {
    $("#" + otherItemId).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "ItemId",
        dataSource: { type: "json", transport: { read: { url: '/Stock/GetOtherItem' } } }
    });
};

bindRoles = function (roleId) {
    $("#" + roleId).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/Employee/GetRoles' } } },
    });
};
bindMaritalStatus = function (maritalStatusId) {
    $("#" + maritalStatusId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/Employee/GetMaritalStatus' } } },
    });
};
bindStatus = function (statusId) {
    $("#" + statusId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/Employee/GetStatus' } } },
    });
};
bindEmployeeType = function (employeeTypeId) {
    $("#" + employeeTypeId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/Attendance/GetEmployeeType' } } }
    });
};

bindFertilizer = function (fertilizerId) {
    $("#" + fertilizerId).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/Expense/GetFertilizers' } } }
    });
};

bindCropName = function (cropNameId) {
    $("#" + cropNameId).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/Expense/GetCropNames' } } }
    });
};

bindFuel = function (fuelId) {
    $("#" + fuelId).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/Expense/GetFuelName' } } },
    });
};

bindLoanReturns = function (loanReturnId, loanId) {
    $("#" + loanReturnId).kendoDropDownList({
        optionLabel: "Select",
        dataTextField: "Name",
        dataValueField: "LoanId",
        dataSource: { type: "json", transport: { read: { url: '/CommonDb/GetLoanReturns?loanId=' + loanId } } },
    });
};

bindEmployeePersonalAccounts= function (empReturnId, empId) {
    $("#" + empReturnId).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/CommonDb/GetPersonalAccountName?userId=' + empId } } }
    });
};

bindFarm = function (farmId) {
    $("#" + farmId).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/CommonDb/GetFarmName' } } },
    });
};

bindArea = function (areaId) {
    $("#" + areaId).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/CommonDb/GetAreaName' } } },
    });
};

bindAccount = function (accountId) {
    $("#" + accountId).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/CommonDb/GetAccount' } } },
    });
};
bindVendor = function (vendorId) {
    $("#" + vendorId).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/CommonDb/GetVendor' } } },
    });
};
bindCrop = function (cropId) {
    $("#" + cropId).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: { type: "json", transport: { read: { url: '/CommonDb/GetCrop' } } },
    });
};

bindFarmStatus = function (farmStatusId) {
    $("#" + farmStatusId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/CommonEnum/GetFarmStatus' } } },
    });
};

bindVendorType = function (vendorTypeId) {
    $("#" + vendorTypeId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/CommonEnum/GetVendorType' } } },
    });
};

bindLoanStatus = function (loanStatusId) {
    $("#" + loanStatusId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/CommonEnum/GetLoanStatus' } } },
    });
};

bindTransectionType = function (transactionId) {
    $("#" + transactionId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/CommonEnum/GetTransectionType' } } },
    });
};

bindExpenseType = function (expenseId) {
    $("#" + expenseId).kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: { type: "json", transport: { read: { url: '/CommonEnum/GetExpense' } } },
    });
};