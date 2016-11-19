/*
 * form_validation.js
 *
 * Demo JavaScript used on Validation-pages.
 */

"use strict";

$(document).ready(function () {

    //===== Validation =====//
    // @see: for default options, see assets/js/plugins.form-components.js (initValidation())

    $.extend($.validator.defaults, {
        invalidHandler: function (form, validator) {
            var errors = validator.numberOfInvalids();
            if (errors) {
                var message = errors == 1
				? 'You missed 1 field. It has been highlighted.'
				: 'You missed ' + errors + ' fields. They have been highlighted.';
                noty({
                    text: message,
                    type: 'error',
                    timeout: 2000
                });
            }
        }
    });

    $("#validate-1").validate();
    $("#validate-2").validate();
    $("#validate-3").validate();
    $("#validate-4").validate();

    $("#rolevalidate").validate(); // Role
    $("#formvalidate").validate(); // Form
    $("#changepasswordvalidate").validate(); // Change Password
    $("#staffchangevalidate").validate(); // Change Detail
    $("#Classvalidate").validate(); // Class
    $("#Departmentvalidate").validate(); // Department
    $("#Sectionvalidate").validate(); // Section
    $("#Subjectvalidate").validate(); // Subject
    $("#studentvalidate").validate(); // Student
    $("#staffvalidate").validate(); // Staff
    $("#feecollectionvalidate").validate(); // FeeCollection
    $("#AttendanceValidate").validate(); // Attendance
    $("#StaffAttendancevalidate").validate(); // StaffAttendance
    $("#warningstudent").validate(); //Waring
    $("#Complaintform").validate(); //Complaint
    $("#ComplaintStaffform").validate(); //ComplaintStaff
    $("#feesumform").validate(); // Fee Summery
    $("#feesumstuform").validate(); //Feesummery
    $("#nofeesumform").validate(); // NOFeeSummery
    $("#Institutevalidate").validate(); // School
    $("#Termvalidate").validate(); //Term
    $("#AcademicYearvalidate").validate(); // AcademicYear
    $("#AcademicYearDropdownvalidate").validate();
    $("#nonteachingvalidate").validate();

    $("#SubjectClassvalidate").validate(); // Subject Class Mapping
    $("#SubjectSubjectvalidate").validate(); // Subject Subject Mapping
    $("#PassingCriteriavalidate").validate(); // Subject Subject Mapping

    $("#NameDelaredvalidate").validate(); // Exam Name Delared
    $("#ExamSchedulevalidate").validate(); // Exam Schedule
    $("#ExamIdAssignedvalidate").validate(); // Exam Id Assigne
    $("#MarkEnterdvalidate").validate(); // Mark Enterd
    $("#MarkEnteryStatus").validate(); // Mark Entery Status

    $("#ReChekingform").validate(); // ReChekingform

    $("#AdmissionNoWisevalidate").validate(); // Admission No Wise
    $("#SchoolClassWisevalidate").validate(); // School Class Wise

    $("#StudentReportvalidate").validate(); // Student Report
    $("#StaffReportvalidate").validate(); // Staff Report
    $("#feesummaryDateReportvalidate").validate(); // Fee Summary Report
    $("#InstituteWiseClassReportvalidate").validate(); // Institute Wise Class
    $("#InstituteWiseStaffReportvalidate").validate(); // Institute Wise Staff
    $("#InstituteClassWiseStaffReportvalidate").validate(); // Institute CLass Wise Staff
    $("#AllTermResultvalidate").validate(); // Institute Wise Staff
    $("#StudentIdAssignedResultvalidate").validate(); // Institute Wise Staff
    $("#nofeesummaryDateReportvalidate").validate(); // No Fee Report
    $("#StaffClassReportvalidate").validate(); // Staff Class Report
    $("#formStudentTitleRepot").validate(); // Student Title Repot

    $("#Batchvalidate").validate(); // Batch Master
    $("#teachertrainnigvalidate").validate(); // Teacher Training
    $("#formCollectionFee").validate(); // Collection Fee
});