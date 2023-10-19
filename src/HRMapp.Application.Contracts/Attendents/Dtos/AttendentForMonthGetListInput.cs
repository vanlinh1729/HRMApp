using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Attendents.Dtos;

[Serializable]
public class AttendentForMonthGetListInput : PagedAndSortedResultRequestDto
{
    public Guid? EmployeeId { get; set; }

    public DateTime? Month { get; set; }
}