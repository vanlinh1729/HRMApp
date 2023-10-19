using System;
using System.ComponentModel.DataAnnotations;
using HRMapp.Attendents;

namespace HRMapp.Web.Pages.Attendents.Attendent.ViewModels;

public class CreateEditAttendentViewModel
{
    [Display(Name = "Date")]
    public DateTime Date { get; set; }=DateTime.Now;

    [Display(Name = "EmployeeName")]
    public Guid EmployeeId { get; set; }

    [Display(Name = "TypeLine")] 
    public TypeLine Type { get; set; }
}
