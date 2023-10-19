using System;

namespace HRMapp.Employees.Dtos;

[Serializable]
public class CreateUpdateEmployeeHistoryDto
{
    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public string Organization { get; set; }

    public string Description { get; set; }
}