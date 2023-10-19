using System;

namespace HRMapp.Shifts.Dtos;

[Serializable]
public class CreateUpdateShiftDto
{
    public string Name { get; set; }

    public TimeSpan Start { get; set; }

    public TimeSpan End { get; set; }

    public TimeSpan TimeStartCheckin { get; set; }

    public TimeSpan TimeStopCheckout { get; set; }
}