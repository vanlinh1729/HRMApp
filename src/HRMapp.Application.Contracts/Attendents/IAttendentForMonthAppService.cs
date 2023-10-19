using System;
using HRMapp.Attendents.Dtos;
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

}