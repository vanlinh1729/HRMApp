using System;
using System.Threading.Tasks;
using HRMapp.Attendents.Dtos;
using HRMapp.Employees.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Attendents;


public interface IAttendentAppService :
    ICrudAppService< 
        AttendentDto, 
        Guid, 
        AttendentGetListInput,
        CreateUpdateAttendentDto,
        CreateUpdateAttendentDto>
{
    Task<ListResultDto<SelectResultDto>> GetListEmployeeAsync();
    Task<ListResultDto<SelectResultDto>> GetListDepartmentAsync();
    Task<AttendentDto> CreateManyAttendentAsync(CreateManyAttendentDto input);
    Task<PagedResultDto<EmployeeWithDetailsDto>> GetAllEmployeeIntoAttendent(AllEmployeeDto input);

}