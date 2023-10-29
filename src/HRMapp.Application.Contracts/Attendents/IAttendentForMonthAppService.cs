using System;
using System.Threading.Tasks;
using HRMapp.Attendents.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Attendents;


public interface IAttendentForMonthAppService :
    ICrudAppService< 
        AttendentForMonthDto, 
        Guid, 
        AttendentForMonthGetListInput,
        CreateUpdateAttendentForMonthDto,
        CreateUpdateAttendentForMonthDto>
{
    Task<ListResultDto<SelectResultDto>> GetListEmployeeAsync();

}