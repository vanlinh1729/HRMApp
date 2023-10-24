using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.Contacts.Dtos;
using HRMapp.Permissions;
using HRMapp.Contracts.Dtos;
using HRMapp.Employees;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
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
    private readonly IEmployeeRepository _employeeRepository;


    public ContractAppService(IContractRepository repository, IEmployeeRepository employeeRepository) : base(repository)
    {
        _employeeRepository = employeeRepository;
        _repository = repository;
    }
    // protected override async Task<IQueryable<Contract>> CreateFilteredQueryAsync(ContractGetListInput input)
    // {
    //     // TODO: AbpHelper generated
    //     return (await base.CreateFilteredQueryAsync(input))
    //         .WhereIf(input.EmployeeId != null, x => x.EmployeeId == input.EmployeeId)
    //         .WhereIf(input.TimeContract != null, x => x.TimeContract == input.TimeContract)
    //         .WhereIf(input.SignDate != null, x => x.SignDate == input.SignDate)
    //         .WhereIf(input.CoefficientSalary != null, x => x.CoefficientSalary == input.CoefficientSalary)
    //         ;
    // }
    
    [Authorize(HRMappPermissions.Contract.Default)]
    public override async Task<PagedResultDto<ContractDto>> GetListAsync(ContractGetListInput input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = from Contract in queryable
            join employee in await _employeeRepository.GetQueryableAsync() on Contract.EmployeeId equals
                employee.Id into employeeinContract
            from employeeinContracts in employeeinContract.DefaultIfEmpty()
            select new ContractDto()
            {
                Id = Contract.Id,
                EmployeeName = employeeinContracts.Name,
                EmployeeId = Contract.EmployeeId,
                CoefficientSalary = Contract.CoefficientSalary,
                TimeContract = Contract.TimeContract,
                SignDate = Contract.SignDate
               
            };
        var listContract = query
            .WhereIf(!input.EmployeeName.IsNullOrWhiteSpace(), x => x.EmployeeName.ToLower().Contains(input.EmployeeName.ToLower()))
            .WhereIf(input.CoefficientSalary != null, x => x.CoefficientSalary == input.CoefficientSalary)
            .WhereIf(input.SignDate!=null,
                x => x.SignDate == input.SignDate)
            .WhereIf(input.TimeContract!=null,
                x => x.TimeContract == input.TimeContract)
            .OrderBy(x=>NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);
        
        
        var queryResult = await AsyncExecuter.ToListAsync(listContract);

        var totalCount = await Repository.GetCountAsync();
        return new PagedResultDto<ContractDto>(
            totalCount,
            queryResult
        );
    }
    
    public async Task<ContractDto> GetContractDetail(Guid ContractId)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = from Contract in queryable
            where Contract.Id == ContractId
            join employee in await _employeeRepository.GetQueryableAsync() on Contract.EmployeeId equals
                employee.Id into employeeinContract
            from employeeinContracts in employeeinContract.DefaultIfEmpty()
            select new ContractDto()
            {
                Id = Contract.Id,
                EmployeeName = employeeinContracts.Name,
                EmployeeId = Contract.EmployeeId,
                CoefficientSalary = Contract.CoefficientSalary,
                TimeContract = Contract.TimeContract,
                SignDate = Contract.SignDate
               
            };
        
        var queryResult = await AsyncExecuter.FirstAsync(query);

        return queryResult;
    }

    
    [Authorize(HRMappPermissions.Contract.Default)]
    public async Task<ListResultDto<SelectResultDto>> GetListEmployees()
    {
        var obj = await _employeeRepository.GetListAsync();
        return new ListResultDto<SelectResultDto>(ObjectMapper.Map<List<Employee>, List<SelectResultDto>>(obj));
    }
   

    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"contract.{nameof(Contract.Id)}";
        }
        // custom contain sorting 
        if (sorting.Contains("employeeName", StringComparison.OrdinalIgnoreCase))
        {
            return "employeeName";
        }
        return $"contract.{sorting}";
    }
}
