using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.Permissions;
using HRMapp.Salarys.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Salarys;


public class SalaryAppService : CrudAppService<Salary, SalaryDto, Guid, SalaryGetListInput, CreateUpdateSalaryDto, CreateUpdateSalaryDto>,
    ISalaryAppService
{
    protected override string GetPolicyName { get; set; } = HRMappPermissions.Salary.Default;
    protected override string GetListPolicyName { get; set; } = HRMappPermissions.Salary.Default;
    protected override string CreatePolicyName { get; set; } = HRMappPermissions.Salary.Create;
    protected override string UpdatePolicyName { get; set; } = HRMappPermissions.Salary.Update;
    protected override string DeletePolicyName { get; set; } = HRMappPermissions.Salary.Delete;

    private readonly ISalaryRepository _repository;

    public SalaryAppService(ISalaryRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override async Task<IQueryable<Salary>> CreateFilteredQueryAsync(SalaryGetListInput input)
    {
        // TODO: AbpHelper generated
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(input.EmployeeId != null, x => x.EmployeeId == input.EmployeeId)
            .WhereIf(input.AttendentForMonthId != null, x => x.AttendentForMonthId == input.AttendentForMonthId)
            ;
    }
}
