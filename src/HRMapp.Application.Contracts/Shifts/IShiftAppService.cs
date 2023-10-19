using System;
using HRMapp.Shifts.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Shifts;


public interface IShiftAppService :
    ICrudAppService< 
        ShiftDto, 
        Guid, 
        ShiftGetListInput,
        CreateUpdateShiftDto,
        CreateUpdateShiftDto>
{

}