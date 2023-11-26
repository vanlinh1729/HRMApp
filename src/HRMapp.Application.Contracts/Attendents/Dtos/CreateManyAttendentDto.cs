using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace HRMapp.Attendents.Dtos;

[Serializable]
public class CreateManyAttendentDto
{
    public DateTime Date { get; set; }
    
    public TypeLine Type { get; set; }
    [CanBeNull] public Guid[] employeeId { get; set; }
}