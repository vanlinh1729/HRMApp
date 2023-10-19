using System;
using System.Collections.Generic;

namespace HRMapp.Attendents.Dtos;

[Serializable]
public class CreateUpdateAttendentDto
{
    public DateTime Date { get; set; }

    public Guid EmployeeId { get; set; }

    public TypeLine Type { get; set; }
}