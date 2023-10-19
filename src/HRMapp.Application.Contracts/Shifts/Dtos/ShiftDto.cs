using System;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Shifts.Dtos;

[Serializable]
public class ShiftDto : EntityDto<Guid>
{
    public string Name { get; set; }

    public TimeSpan Start { get; set; }

    public TimeSpan End { get; set; }

    public TimeSpan TimeStartCheckin { get; set; }

    public TimeSpan TimeStopCheckout { get; set; }
}