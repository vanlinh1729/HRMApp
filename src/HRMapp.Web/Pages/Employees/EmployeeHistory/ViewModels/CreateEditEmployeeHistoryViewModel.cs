using System;
using System.ComponentModel.DataAnnotations;

namespace HRMapp.Web.Pages.Employees.EmployeeHistory.ViewModels;

public class CreateEditEmployeeHistoryViewModel
{
    [Display(Name = "EmployeeHistoryStart")]
    public DateTime Start { get; set; }

    [Display(Name = "EmployeeHistoryEnd")]
    public DateTime End { get; set; }

    [Display(Name = "EmployeeHistoryOrganization")]
    public string Organization { get; set; }

    [Display(Name = "EmployeeHistoryDescription")]
    public string Description { get; set; }
}
