using System;

namespace HRMapp.Attendents.Dtos;

[Serializable]
public class CreateUpdateAttendentLineDto
{
    public Guid AttendentId { get; set; }

    public DateTime TimeCheck { get; set; }

    public TypeLine Type { get; set; }

    public Guid ShiftId { get; set; }
}