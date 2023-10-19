using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRMapp.Departments.Dtos;

[Serializable]
public class CreateUpdateDepartmentDto
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    public Guid? OwnerId { get; set; }

    public Guid? ParentId { get; set; }

}