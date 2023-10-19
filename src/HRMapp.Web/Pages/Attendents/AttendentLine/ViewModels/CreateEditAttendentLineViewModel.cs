using System;
using System.ComponentModel.DataAnnotations;
using HRMapp.Attendents;

namespace HRMapp.Web.Pages.Attendents.AttendentLine.ViewModels;

public class CreateEditAttendentLineViewModel
{
    [Display(Name = "AttendentLineAttendentId")]
    public Guid AttendentId { get; set; }

    [Display(Name = "AttendentLineTimeCheck")]
    public DateTime TimeCheck { get; set; }

    [Display(Name = "AttendentLineType")]
    public TypeLine Type { get; set; }

    [Display(Name = "AttendentLineShiftId")]
    public Guid ShiftId { get; set; }
}
