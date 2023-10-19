using System;

namespace HRMapp.Departments.Dtos;

public class DepartmentChangeOwnerDto
{
    public string UserName { get; set; }
    public string NewValue { get; set; }
    public string OriginalValue { get; set; }
    public DateTime ChangeTime { get; set; }
}