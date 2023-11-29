using System;
using System.Collections.Generic;
using HRMapp.Employees;

namespace HRMapp.Attendents.Dtos;

public class AttendentForMonthDetailDto
{
    public DateTime Date { get; set; }

    public string EmployeeName { get; set; }
    public EmployeePosition EmployeePosition { get; set; }
    public Guid EmployeeId { get; set; }
    public string DepartmentName { get; set; }
    public decimal CountAtt { get; set; }
    public List<AttendentLineDto> AttendentLines { get; set; }
    
    public List<DayCheckDto> DayCheckDtos { get; set; }
    
}