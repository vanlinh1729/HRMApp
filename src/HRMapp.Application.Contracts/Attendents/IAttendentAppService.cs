using System;
using HRMapp.Attendents.Dtos;
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

}