using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.Permissions;
using HRMapp.Employees.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Employees;


public class EmployeeHistoryAppService : CrudAppService<EmployeeHistory, EmployeeHistoryDto, Guid, EmployeeHistoryGetListInput, CreateUpdateEmployeeHistoryDto, CreateUpdateEmployeeHistoryDto>,
    IEmployeeHistoryAppService
{
    protected override string GetPolicyName { get; set; } = HRMappPermissions.EmployeeHistory.Default;
    protected override string GetListPolicyName { get; set; } = HRMappPermissions.EmployeeHistory.Default;
    protected override string CreatePolicyName { get; set; } = HRMappPermissions.EmployeeHistory.Create;
    protected override string UpdatePolicyName { get; set; } = HRMappPermissions.EmployeeHistory.Update;
    protected override string DeletePolicyName { get; set; } = HRMappPermissions.EmployeeHistory.Delete;

    private readonly IEmployeeHistoryRepository _repository;

    public EmployeeHistoryAppService(IEmployeeHistoryRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override async Task<IQueryable<EmployeeHistory>> CreateFilteredQueryAsync(EmployeeHistoryGetListInput input)
    {
        // TODO: AbpHelper generated
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(input.Start != null, x => x.Start == input.Start)
            .WhereIf(input.End != null, x => x.End == input.End)
            .WhereIf(!input.Organization.IsNullOrWhiteSpace(), x => x.Organization.Contains(input.Organization))
            .WhereIf(!input.Description.IsNullOrWhiteSpace(), x => x.Description.Contains(input.Description))
            ;
    }
}
