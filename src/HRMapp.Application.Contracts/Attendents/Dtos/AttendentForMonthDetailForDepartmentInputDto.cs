using System;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Attendents.Dtos;

public class AttendentForMonthDetailForDepartmentInputDto
{
    public DateTime Date { get; set; }
    public string DepartmentName { get; set; }
    
}