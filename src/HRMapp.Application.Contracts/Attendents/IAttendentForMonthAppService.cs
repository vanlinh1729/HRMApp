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
    Task<AttendentForMonthDto> CreateManyAttendentForMonthAsync(CreateManyAttendentForMonthDto input);
    Task<AttendentForMonthDto> GetAttendentForMonthDetail(Guid att4mId);
    Task<AllAttendentForMonthDto> GetListManyAttendentForMonthAsync(CreateManyAttendentForMonthDto input);

}