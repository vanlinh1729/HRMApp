using System;
using System.ComponentModel.DataAnnotations;
using HRMapp.Employees;

namespace HRMapp.Web.Pages.Employees.Employee.ViewModels;

public class CVofEmployeeViewModel
{
    [Display(Name = "EmployeeName")]
    public string Name { get; set; }

    [Display(Name = "EmployeeOtherName")]
    public string OtherName { get; set; }
    
    public string UserName { get; set; }

    [Display(Name = "EmployeeHrmUserId")]
    public Guid? UserId { get; set; }

    public string ContactName { get; set; }
    
    [Display(Name = "EmployeeHrmContactId")]
    public Guid? ContactId { get; set; }

    [Display(Name = "EmployeeDepartmentId")]
    public Guid? DepartmentId { get; set; }

    [Display(Name = "EmployeeStatus")]
    public StatusEmployee Status { get; set; }
}