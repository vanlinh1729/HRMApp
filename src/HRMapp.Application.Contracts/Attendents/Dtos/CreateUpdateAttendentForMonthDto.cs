using System;

namespace HRMapp.Attendents.Dtos;

[Serializable]
public class CreateUpdateAttendentForMonthDto
{
    public Guid EmployeeId { get; set; }

    public DateTime Month { get; set; }
}