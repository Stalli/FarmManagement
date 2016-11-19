using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
    public class TaskAssignmentModel
    {
        public Int32 Id { get; set; }

        [Required]
        [Display(Name = "Task Name")]
        public string TaskName { get; set; }

        [Required]
        [Display(Name = "Staff Name")]
        public Int32 StaffNameId { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Deadline Start Date")]
        public DateTime DeadlineStartDate { get; set; }

        [Required]
        [Display(Name = "Deadline End Date")]
        public DateTime DeadlineEndDate { get; set; }

        [Display(Name = "Status")]
        public Int32? TaskStatus { get; set; }

        [Display(Name = "Remarks")]
        public Int32? TaskRemarks { get; set; }

        public string StaffName { get; set; }
        public string TaskRemarkReport { get; set; }
        public string TaskStatusReport { get; set; }

    }
}