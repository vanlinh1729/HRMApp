using System;
using System.Threading.Tasks;
using HRMapp.Employees.Dtos;
using Volo.Abp.Application.Dtos;
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

    Task<ListResultDto<SelectResultDto>> GetListEmployees();
    Task<EmployeeHistoryDto> GetEmployeeHistoryDetail(Guid employeeHistoryId);
}