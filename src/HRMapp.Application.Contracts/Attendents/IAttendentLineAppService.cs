using System;
using HRMapp.Attendents.Dtos;
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

}