using System;

namespace HRMapp.Employees.Dtos;

[Serializable]
public class CreateUpdateEmployeeDto
{
    public string Name { get; set; }

    public string OtherName { get; set; }

    public Guid? UserId { get; set; }

    public Guid? ContactId { get; set; }

    public Guid? DepartmentId { get; set; }

    public StatusEmployee Status { get; set; }

}