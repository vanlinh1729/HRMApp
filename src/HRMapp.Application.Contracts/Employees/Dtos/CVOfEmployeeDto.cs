using System;
using System.Collections.Generic;
using HRMapp.Contacts;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Employees.Dtos;

public class CVOfEmployeeDto 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string OtherName { get; set; }
    public string UserName { get; set; }
    public Guid? UserId { get; set; }
    [CanBeNull] public string ContactName { get; set; }
    [CanBeNull] public string ContactAddress { get; set; }
    public Guid? ContactId { get; set; }
    public string DepartmentName { get; set; }
    public Guid? DepartmentId { get; set; }
    public StatusEmployee Status { get; set; }
    
    public  Gender? Gender { get; set; }
    public DateTime? BirthDay { get; set; }
    [CanBeNull] public string Email { get;  set; }
    [CanBeNull] public string PhoneNumber { get;  set; }
    public List<EmployeeHistoryDto> EmployeeHistories { get; set; }
}