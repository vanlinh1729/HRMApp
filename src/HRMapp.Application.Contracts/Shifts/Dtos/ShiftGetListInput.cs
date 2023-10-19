using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Shifts.Dtos;

[Serializable]
public class ShiftGetListInput : PagedAndSortedResultRequestDto
{
    public string? Name { get; set; }

    public TimeSpan? Start { get; set; }

    public TimeSpan? End { get; set; }

    public TimeSpan? TimeStartCheckin { get; set; }

    public TimeSpan? TimeStopCheckout { get; set; }
}