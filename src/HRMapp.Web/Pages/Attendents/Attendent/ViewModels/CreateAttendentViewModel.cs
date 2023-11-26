using System;
using System.ComponentModel.DataAnnotations;
using HRMapp.Attendents;
using JetBrains.Annotations;

namespace HRMapp.Web.Pages.Attendents.Attendent.ViewModels;

public class CreateManyAttendentViewModel
{
    [Display(Name = "Date")]
    public DateTime Date { get; set; }=DateTime.Now;
    
    [Display(Name = "TypeLine")] 
    public TypeLine Type { get; set; }
    
    [CanBeNull] public Guid[] employeeId { get; set; }
}