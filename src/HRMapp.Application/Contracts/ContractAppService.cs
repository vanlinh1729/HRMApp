using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.Contacts.Dtos;
using HRMapp.Permissions;
using HRMapp.Contracts.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Contracts;


public class ContractAppService : CrudAppService<Contract, ContractDto, Guid, ContractGetListInput, CreateUpdateContractDto, CreateUpdateContractDto>,
    IContractAppService
{
    protected override string GetPolicyName { get; set; } = HRMappPermissions.Contract.Default;
    protected override string GetListPolicyName { get; set; } = HRMappPermissions.Contract.Default;
    protected override string CreatePolicyName { get; set; } = HRMappPermissions.Contract.Create;
    protected override string UpdatePolicyName { get; set; } = HRMappPermissions.Contract.Update;
    protected override string DeletePolicyName { get; set; } = HRMappPermissions.Contract.Delete;

    private readonly IContractRepository _repository;

    public ContractAppService(IContractRepository repository) : base(repository)
    {
        _repository = repository;
    }
    protected override async Task<IQueryable<Contract>> CreateFilteredQueryAsync(ContractGetListInput input)
    {
        // TODO: AbpHelper generated
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(input.EmployeeId != null, x => x.EmployeeId == input.EmployeeId)
            .WhereIf(input.TimeContract != null, x => x.TimeContract == input.TimeContract)
            .WhereIf(input.SignDate != null, x => x.SignDate == input.SignDate)
            .WhereIf(input.CoefficientSalary != null, x => x.CoefficientSalary == input.CoefficientSalary)
            ;
    }
   

}
