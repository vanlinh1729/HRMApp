using System;
using System.Collections.Generic;
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

    Task<List<AttendentForMonthDetailDto>> GetListAttendentForMonthDetailAsync(AttendentForMonthDetailInputDto input);

    Task<List<AttendentForMonthDetailDto>> GetListAttendentForMonthForDepartmentDetailAsync(
        AttendentForMonthDetailForDepartmentInputDto input);

    Task<ListResultDto<SelectResultDto>> GetListDepartmentAsync();

}