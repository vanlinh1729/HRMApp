using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Departments.Dtos;

[Serializable]
public class DepartmentGetListInput : PagedAndSortedResultRequestDto
{
    public string? Name { get; set; }

    public string? OwnerName { get; set; }
    
    public Guid? OwnerId { get; set; }
    
    public string? ParentName { get; set; }
    
    public Guid? ParentId { get; set; }
}