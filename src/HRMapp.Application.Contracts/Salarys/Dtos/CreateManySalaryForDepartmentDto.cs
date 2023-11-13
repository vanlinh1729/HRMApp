using System;

namespace HRMapp.Salarys.Dtos;

[Serializable]
public class CreateManySalaryForMonthDto
{
    public DateTime AttendentForMonthMonth { get; set; }
     public Guid DepartmentId { get; set; }
}