using System;
using System.Threading.Tasks;
using HRMapp.Employees.Dtos;
using Microsoft.AspNetCore.Http;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Employees;


public interface IEmployeeAppService :
    ICrudAppService< 
        EmployeeDto, 
        Guid, 
        EmployeeGetListInput,
        CreateUpdateEmployeeDto,
        CreateUpdateEmployeeDto>
{

    Task<ListResultDto<SelectResultDto>> GetListHrmUserAsync();

    Task<ListResultDto<SelectResultDto>> GetListHrmContactAsync();

    Task<ListResultDto<SelectResultDto>> GetListDepartmentAsync();
    Task<EmployeeDto> GetEmployeeDetail(Guid departmentId);
    Task<CVOfEmployeeDto> GetCVofEmployee(Guid departmentId);
    Task<string> UpdateDepartment(EmployeeInputUpdateOneFieldDto input);
    Task<int> EmployeeCountAsync();
    Task<EmployeeDto> ImportEmployeeFromExcelAsync(IFormFile excel);
}