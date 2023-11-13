using System;
using System.ComponentModel.DataAnnotations;

namespace HRMapp.Web.Pages.Salarys.Salary.ViewModels;

public class CreateSalaryForDepartmentViewModel
{
    [Display(Name = "SalaryAttendentForMonthMonth")]
    public DateTime AttendentForMonthMonth { get; set; } 
   
    [Display(Name = "DepartmentName")]
    public Guid? DepartmentId { get; set; } 
     
    [Display(Name = "DepartmentName")]
    public string? DepartmentName { get; set; }
}
