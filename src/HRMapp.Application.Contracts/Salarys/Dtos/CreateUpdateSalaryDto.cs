using System;

namespace HRMapp.Salarys.Dtos;

[Serializable]
public class CreateUpdateSalaryDto
{
    public Guid? EmployeeId { get; set; }

    public Guid? AttendentForMonthId { get; set; }
}