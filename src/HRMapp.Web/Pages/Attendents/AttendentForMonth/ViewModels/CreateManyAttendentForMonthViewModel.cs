using System;
using System.ComponentModel.DataAnnotations;

namespace HRMapp.Web.Pages.Attendents.AttendentForMonth.ViewModels;

public class CreateManyAttendentForMonthViewModel
{
    [Display(Name = "AttendentForMonthMonth")]
    public DateTime Month { get; set; } 
   
}
