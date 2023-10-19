using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace HRMapp.Web.Pages.Departments.Department.ViewModels;

public class CreateEditDepartmentViewModel
{
    [Display(Name = "DepartmentName")]
    public string Name { get; set; }

    [Display(Name = "DepartmentOwnerId")]
    public Guid? OwnerId { get; set; }

    [Display(Name = "DepartmentParentId")]
    public Guid? ParentId { get; set; }
    
    [CanBeNull] public string OwnerName { get; set; }
    
    [CanBeNull] public string ParentName { get; set; }

    [CanBeNull] public Guid[] employeeId { get; set; }
}
