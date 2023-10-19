using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Attendents.Dtos;

[Serializable]
public class AttendentLineGetListInput : PagedAndSortedResultRequestDto
{
    public string AttendentName { get; set; }
    
    public Guid? AttendentId { get; set; }
    
    public DateTime? TimeCheck { get; set; }
    
    public TypeLine? Type { get; set; }
    
    public string ShiftName { get; set; }
    
    public Guid? ShiftId { get; set; }
}