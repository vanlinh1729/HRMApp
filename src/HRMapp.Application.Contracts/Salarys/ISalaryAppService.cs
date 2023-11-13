using System;
using System.Threading.Tasks;
using HRMapp.Salarys.Dtos;
using Volo.Abp.Application.Dtos;
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

    Task<ListResultDto<SelectResultDto>> GetListEmployeeAsync();
    Task<SalaryDto> CreateManySalaryAsync(CreateManySalaryDto input);
    Task<SalaryDto> GetSalaryDetail(Guid salaryId);
    Task<SalaryForMonthDto> GetListSalaryForMonthAsync(CreateManySalaryDto input);

    Task<SalaryForMonthDto> GetListSalaryForMonthForDepartmentAsync(CreateManySalaryForMonthDto input);
}