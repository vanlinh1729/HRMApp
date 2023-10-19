using System;
using HRMapp.Salarys.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Salarys;


public interface ISalaryAppService :
    ICrudAppService< 
        SalaryDto, 
        Guid, 
        SalaryGetListInput,
        CreateUpdateSalaryDto,
        CreateUpdateSalaryDto>
{

}