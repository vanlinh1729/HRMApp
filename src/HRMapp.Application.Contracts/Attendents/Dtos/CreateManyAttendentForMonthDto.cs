using System;

namespace HRMapp.Attendents.Dtos;

[Serializable]
public class CreateManyAttendentForMonthDto
{
    public DateTime Month { get; set; }
}