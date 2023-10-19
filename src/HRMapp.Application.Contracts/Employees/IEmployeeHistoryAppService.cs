using System;
using HRMapp.Employees.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Employees;


public interface IEmployeeHistoryAppService :
    ICrudAppService< 
        EmployeeHistoryDto, 
        Guid, 
        EmployeeHistoryGetListInput,
        CreateUpdateEmployeeHistoryDto,
        CreateUpdateEmployeeHistoryDto>
{

}