using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRMapp.Departments.Dtos;
using HRMapp.Employees.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Departments;


public interface IDepartmentAppService :
    ICrudAppService< 
        DepartmentDto, 
        Guid, 
        DepartmentGetListInput,
        CreateUpdateDepartmentDto,
        CreateUpdateDepartmentDto>
{

    Task<PagedResultDto<DepartmentDto>> GetListAsync(DepartmentGetListInput input);
    Task<DepartmentDto> GetDepartmentDetail(Guid departmentId);
    Task<ListResultDto<SelectResultDto>> GetListParentAsync();
    Task<ListResultDto<SelectResultDto>> GetListOwnerAsync();
    Task<List<EmployeeWithName>> GetListEmployeeNameDepartment(Guid departmentId);
    Task<DepartmentDto> CreateDepartmentWithManyEmployeeAsync(CreateDepartmentAndAddEmployee input);
    Task<DepartmentDto> UpdateDepartmentWithManyEmployeeAsync(Guid departmentId, CreateDepartmentAndAddEmployee input);
    Task<List<DepartmentChangeOwnerDto>> GetDepartmentChangeListAsync(Guid departmentId);
    Task<PagedResultDto<DepartmentWithDetailDto>> GetListUsersDepartmentEdit(DepartmentDetailById input);
}