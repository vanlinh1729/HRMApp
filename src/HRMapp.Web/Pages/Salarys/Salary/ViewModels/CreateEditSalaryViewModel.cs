using System;
using System.ComponentModel.DataAnnotations;

namespace HRMapp.Web.Pages.Salarys.Salary.ViewModels;

public class CreateEditSalaryViewModel
{
    [Display(Name = "SalaryEmployeeId")]
    public Guid? EmployeeId { get; set; }

    [Display(Name = "SalaryAttendentForMonthId")]
    public Guid? AttendentForMonthId { get; set; } 
    
    [Display(Name = "SalaryAttendentForMonthMonth")]
    public DateTime? AttendentForMonthMonth { get; set; } 
    
    [Display(Name = "SalaryTotalSalary")]
    public decimal? TotalSalary { get; set; }
}
