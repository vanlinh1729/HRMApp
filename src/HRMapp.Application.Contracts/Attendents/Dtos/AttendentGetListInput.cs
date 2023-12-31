using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Attendents.Dtos;

[Serializable]
public class AttendentGetListInput : PagedAndSortedResultRequestDto
{
    public string? Date { get; set; }
    public string? Datetime { get; set; }
    public string? EmployeeName { get; set; }
    public Guid? EmployeeId { get; set; }
    public int? MissingIn { get; set; }
    public int? MissingOut { get; set; }
    public int? TimeMissingIn { get; set; }
    public int? TimeMissingOut { get; set; }
    public int MaxResultCount { get; set; } = (int)999999999;

}