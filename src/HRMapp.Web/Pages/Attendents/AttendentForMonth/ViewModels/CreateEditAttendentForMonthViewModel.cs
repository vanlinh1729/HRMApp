using System;
using System.ComponentModel.DataAnnotations;

namespace HRMapp.Web.Pages.Attendents.AttendentForMonth.ViewModels;

public class CreateEditAttendentForMonthViewModel
{
    [Display(Name = "AttendentForMonthEmployeeId")]
    public Guid EmployeeId { get; set; }

    [Display(Name = "AttendentForMonthMonth")]
    public DateTime Month { get; set; }
}
