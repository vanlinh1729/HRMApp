using System;

namespace HRMapp.Employees.Dtos;

[Serializable]
public class CreateUpdateEmployeeHistoryDto
{
    public string EmployeeName { get; set; }
    
    public Guid EmployeeId { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public string Organization { get; set; }

    public string Description { get; set; }
}