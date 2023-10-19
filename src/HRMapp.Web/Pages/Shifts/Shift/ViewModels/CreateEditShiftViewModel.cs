using System;
using System.ComponentModel.DataAnnotations;

namespace HRMapp.Web.Pages.Shifts.Shift.ViewModels;

public class CreateEditShiftViewModel
{
    [Display(Name = "ShiftName")]
    public string Name { get; set; }

    [Display(Name = "ShiftStart")]
    public TimeSpan Start { get; set; }

    [Display(Name = "ShiftEnd")]
    public TimeSpan End { get; set; }

    [Display(Name = "ShiftTimeStartCheckin")]
    public TimeSpan TimeStartCheckin { get; set; }

    [Display(Name = "ShiftTimeStopCheckout")]
    public TimeSpan TimeStopCheckout { get; set; }
}
