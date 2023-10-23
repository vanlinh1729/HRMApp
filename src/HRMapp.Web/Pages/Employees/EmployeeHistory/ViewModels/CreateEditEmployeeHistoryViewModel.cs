using System;
using System.ComponentModel.DataAnnotations;

namespace HRMapp.Web.Pages.Employees.EmployeeHistory.ViewModels;

public class CreateEditEmployeeHistoryViewModel
{
    [Display(Name = "EmployeeHistoryEmployeeName")]
    public string EmployeeName { get; set; }
    
    [Display(Name = "EmployeeHistoryEmployeeId")]
    public Guid EmployeeId { get; set; }
    
    [Display(Name = "EmployeeHistoryStart")]
    public DateTime Start { get; set; }

    [Display(Name = "EmployeeHistoryEnd")]
    public DateTime End { get; set; }

    [Display(Name = "EmployeeHistoryOrganization")]
    public string Organization { get; set; }

    [Display(Name = "EmployeeHistoryDescription")]
    public string Description { get; set; }
}
