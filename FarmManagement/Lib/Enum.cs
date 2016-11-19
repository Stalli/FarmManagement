using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FarmManagement.Lib
{
    public enum FarmStatus
    {
        Close = 1,
        Pending = 2,
        InUse = 3
    }

    public enum VendorType
    {
        Consultant = 1,
        ServiceProvider = 2,
        Suppliers = 3,
        Supplies = 4,
        TaxAgency = 5
    }

    public enum EmployeeType
    {
        Permanent = 1,
        Temporary = 2,
        Vendor = 3
    }

    public enum EmployeeStatus
    {
        Active = 1,
        Block = 2
    }

    public enum TaskStatus
    {
        Active = 1,
        OverDeadline = 2,
        Complete = 3
    }

    public enum TaskRemarks
    {
        Excellent = 1,
        Good = 2,
        Average = 3,
        BelowAverage = 4,
        NotAcceptable = 5
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public enum MaritalStatus
    {
        Married = 1,
        UnMarried = 2
    }

    public enum AnimalCategory
    {
        Shap = 1,
        Cow = 2,
        Dog = 3,
        Bufflo = 4,
        Horse = 5
    }

    public enum FileUploadPath
    {
        Animal,
        Fertilizer,
        Fuel,
        Seed,
        MachinePicture,
        MachineLicenseFile,
        VehiclePicture,
        VehicleLicenseFile,
        EmployeePicture,
        EmployeeScannedDocument,
        EmployeeCNICFrontDocument,
        EmployeeCNICBackDocument,
        EmployeeAgreementDocument
    }

    public enum Attendance
    {
        IsPresentMorning,
        IsLeaveMorning,
        IsAbsentMorning,
        IsPresentNight,
        IsLeaveNight,
        IsAbsentNight
    }

    public enum Quality
    {
        High = 1,
        Low = 2,
        Medium = 3
    }
    public enum Quantity
    {
        PerBag = 1,
        PerPacket = 2,
        Other = 3
    }

    public enum VehicleType
    {
        Car = 1,
        Bike = 2,
        Tractor = 3,
        Jeap = 4,
        Other
    }

    public enum Condition
    {
        New = 1,
        Old = 2,
        Other = 3
    }

    public enum FertilizerType
    {
        Liquid = 1,
        Solid = 2,
        Other = 3
    }

    public enum FertilizerQuantity
    {
        PerBottle = 1,
        PerBag = 2,
        PerPacket = 3,
        Other = 4
    }

    public enum OtherItemTag
    {
        InAdminOffice = 1,
        FarmHouse = 2
    }

    public enum LoanStatus
    {
        Approve = 1,
        Reject = 2
    }

    public enum LoanReturn
    {
        PerMonth = 1,
        PerYear = 2
    }

    public enum TransectionType
    {
        Credit = 1,
        Debit = 2
    }
    public enum PrintType
    {
        Pdf,
        Excel
    }

    public enum Expense
    {
        Seed,
        Fuel,
        Fertilizer,
        OtherItem
    }
}