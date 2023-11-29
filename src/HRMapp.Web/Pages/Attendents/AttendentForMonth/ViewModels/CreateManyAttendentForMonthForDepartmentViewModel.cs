using System;
using System.ComponentModel.DataAnnotations;

namespace HRMapp.Web.Pages.Attendents.AttendentForMonth.ViewModels;

public class CreateManyAttendentForMonthForDepartmentViewModel
{
    [Display(Name = "AttendentForMonthMonth")]
    public DateTime Month { get; set; } 
    [Display(Name = "DepartmentName")]
    public string? DepartmentName { get; set; } 
   
}
