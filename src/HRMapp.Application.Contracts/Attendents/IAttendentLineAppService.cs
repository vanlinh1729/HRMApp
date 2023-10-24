using System;
using System.Threading.Tasks;
using HRMapp.Attendents.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Attendents;


public interface IAttendentLineAppService :
    ICrudAppService< 
        AttendentLineDto, 
        Guid, 
        AttendentLineGetListInput,
        CreateUpdateAttendentLineDto,
        CreateUpdateAttendentLineDto>
{

    Task<ListResultDto<SelectResultDto>> GetListShifts();
    Task<AttendentLineDto> GetAttendentLineDetail(Guid attendentLineId);

}