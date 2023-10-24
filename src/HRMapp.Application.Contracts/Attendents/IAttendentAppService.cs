using System;
using System.Threading.Tasks;
using HRMapp.Attendents.Dtos;
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

}