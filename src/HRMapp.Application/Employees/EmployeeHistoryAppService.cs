using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.Permissions;
using HRMapp.Employees.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

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
    private readonly IRepository<Employee, Guid> _employeeRepository;


    public EmployeeHistoryAppService(IEmployeeHistoryRepository repository,IRepository<Employee, Guid> employeeRepository) : base(repository)
    {
        _employeeRepository = employeeRepository;
        _repository = repository;
    }

    // protected override async Task<IQueryable<EmployeeHistory>> CreateFilteredQueryAsync(EmployeeHistoryGetListInput input)
    // {
    //     // TODO: AbpHelper generated
    //     return (await base.CreateFilteredQueryAsync(input))
    //         .WhereIf(input.Start != null, x => x.Start == input.Start)
    //         .WhereIf(input.End != null, x => x.End == input.End)
    //         .WhereIf(!input.Organization.IsNullOrWhiteSpace(), x => x.Organization.Contains(input.Organization))
    //         .WhereIf(!input.Description.IsNullOrWhiteSpace(), x => x.Description.Contains(input.Description))
    //         ;
    // }
    
     [Authorize(HRMappPermissions.EmployeeHistory.Default)]
    public override async Task<PagedResultDto<EmployeeHistoryDto>> GetListAsync(EmployeeHistoryGetListInput input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = from employeehistory in queryable
            join employee in await _employeeRepository.GetQueryableAsync() on employeehistory.EmployeeId equals
                employee.Id into employeeinemployeehistory
            from employeeinhistories in employeeinemployeehistory.DefaultIfEmpty()
            select new EmployeeHistoryDto()
            {
                Id = employeehistory.Id,
                EmployeeName = employeeinhistories.Name,
                EmployeeId = employeehistory.EmployeeId,
                Start = employeehistory.Start,
                End = employeehistory.End,
                JobPosition = employeehistory.JobPosition,
                Organization = employeehistory.Organization,
                Description = employeehistory.Description
               
            };
        var listEmployeeHistory = query
            .WhereIf(!input.EmployeeName.IsNullOrWhiteSpace(), x => x.EmployeeName.ToLower().Contains(input.EmployeeName.ToLower()))
            .WhereIf(input.Start!=null,
                x => x.Start == input.Start)
            .WhereIf(input.End!=null,
                x => x.Start == input.End)
            .OrderBy(x=>NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);
        
        
        var queryResult = await AsyncExecuter.ToListAsync(listEmployeeHistory);

        var totalCount = await Repository.GetCountAsync();
        return new PagedResultDto<EmployeeHistoryDto>(
            totalCount,
            queryResult
        );
    }
    public async Task<EmployeeHistoryDto> GetEmployeeHistoryDetail(Guid employeeHistoryId)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = from employeehistory in queryable
            where employeehistory.Id == employeeHistoryId
            join employee in await _employeeRepository.GetQueryableAsync() on employeehistory.EmployeeId equals
                employee.Id into employeeinemployeehistory
            from employeeinhistories in employeeinemployeehistory.DefaultIfEmpty()
            select new EmployeeHistoryDto()
            {
                Id = employeehistory.Id,
                EmployeeName = employeeinhistories.Name,
                EmployeeId = employeehistory.EmployeeId,
                Start = employeehistory.Start,
                End = employeehistory.End,
                JobPosition = employeehistory.JobPosition,
                Organization = employeehistory.Organization,
                Description = employeehistory.Description
               
            };
        
        var queryResult = await AsyncExecuter.FirstAsync(query);

        return queryResult;
    }

    
    [Authorize(HRMappPermissions.EmployeeHistory.Default)]
    public async Task<ListResultDto<SelectResultDto>> GetListEmployees()
    {
        var obj = await _employeeRepository.GetListAsync();
        return new ListResultDto<SelectResultDto>(ObjectMapper.Map<List<Employee>, List<SelectResultDto>>(obj));
    }
    
    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"employeeHistory.{nameof(EmployeeHistory.Id)}";
        }

        // custom contain sorting 
        if (sorting.Contains("employeeName", StringComparison.OrdinalIgnoreCase))
        {
            return "employeeName";
        }
        return $"employeeHistory.{sorting}";
    }
}
