using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Salarys.Dtos;

[Serializable]
public class SalaryGetListInput : PagedAndSortedResultRequestDto
{
    public Guid? EmployeeId { get; set; }

    public Guid? AttendentForMonthId { get; set; }
}