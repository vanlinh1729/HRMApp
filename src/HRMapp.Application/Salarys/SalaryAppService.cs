using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using HRMapp.Attendents;
using HRMapp.Contracts;
using HRMapp.Employees;
using HRMapp.Permissions;
using HRMapp.Salarys.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

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
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IAttendentForMonthRepository _attendentForMonthRepository;
    private readonly IContractRepository _contractRepository;

    public SalaryAppService(ISalaryRepository repository,
        IEmployeeRepository employeeRepository,
        IAttendentForMonthRepository attendentForMonthRepository,
        IContractRepository contractRepository) : base(repository)
    {
        _attendentForMonthRepository = attendentForMonthRepository;
        _employeeRepository = employeeRepository;
        _repository = repository;
        _contractRepository = contractRepository;
    }

    // protected override async Task<IQueryable<Salary>> CreateFilteredQueryAsync(SalaryGetListInput input)
    // {
    //     // TODO: AbpHelper generated
    //     return (await base.CreateFilteredQueryAsync(input))
    //         .WhereIf(input.EmployeeId != null, x => x.EmployeeId == input.EmployeeId)
    //         .WhereIf(input.SalaryForMonthId != null, x => x.SalaryForMonthId == input.SalaryForMonthId)
    //         ;
    // }
    
    
    
     [Authorize(HRMappPermissions.Salary.Default)]
    public override async Task<PagedResultDto<SalaryDto>> GetListAsync(SalaryGetListInput input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = from salary in queryable
            join employee in await _employeeRepository.GetQueryableAsync() on salary.EmployeeId equals
                employee.Id into salaryemployee
            from salaryemployees in salaryemployee.DefaultIfEmpty()
            join attendentformonth in await _attendentForMonthRepository.GetQueryableAsync() on salary.AttendentForMonthId equals attendentformonth.Id
            into salaryattendentformonth
            from salaryattendentformonths in salaryattendentformonth.DefaultIfEmpty()
            
            select new SalaryDto()
            {
                Id = salary.Id,
                EmployeeName = salaryemployees.Name,
                EmployeeId = salary.EmployeeId,
                AttendentForMonthId = salary.AttendentForMonthId,
                AttendentForMonthMonth = salaryattendentformonths.Month,
                TotalSalary = salary.TotalSalary
            };
    
        query = query
            .WhereIf(!input.EmployeeName.IsNullOrWhiteSpace(), x => x.EmployeeName.ToLower().Contains(input.EmployeeName.ToLower()))
            /*
            .WhereIf(input.AttendentForMonthMonth!=null, x => x.AttendentForMonthMonth.GetValueOrDefault().Month == input.AttendentForMonthMonth.GetValueOrDefault().Month && x.AttendentForMonthMonth.GetValueOrDefault().Year == input.AttendentForMonthMonth.GetValueOrDefault().Year)
            */
            .OrderBy(x=>NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);
        var queryResult = await AsyncExecuter.ToListAsync(query);
        var salaryDtos = queryResult.Select(x => new SalaryDto()
        {
            Id = x.Id,
            EmployeeId = x.EmployeeId,
            EmployeeName = x.EmployeeName,
            AttendentForMonthId = x.AttendentForMonthId,
            AttendentForMonthMonth = x.AttendentForMonthMonth,
            TotalSalary = x.TotalSalary
           
        }).ToList();
        var totalCount = await Repository.CountAsync();
        return new PagedResultDto<SalaryDto>(
            totalCount,
            salaryDtos
        );

        /*var query = from salary in (await Repository.GetQueryableAsync())
            join attendentformonth in await _attendentForMonthRepository.GetQueryableAsync() on salary.AttendentForMonthId equals attendentformonth.Id
                into salaryAttendentForMonth
            from salaryAttendentForMonths in salaryAttendentForMonth.DefaultIfEmpty()
            join employee in await _employeeRepository.GetQueryableAsync() on salary.EmployeeId equals employee.Id
                into Salaryemployee
            from employees in Salaryemployee.DefaultIfEmpty()
            select new SalaryDto()
            {
               EmployeeId = salary.EmployeeId,
               EmployeeName = employees.Name,
               AttendentForMonthId = salary.AttendentForMonthId,
               AttendentForMonthMonth = salaryAttendentForMonths.Month,
               TotalSalary = salary.TotalSalary
            };
        var listSalary = query
            .WhereIf(!input.EmployeeName.IsNullOrWhiteSpace(), x => x.EmployeeName.ToLower().Contains(input.EmployeeName.ToLower()))
            .WhereIf(input.AttendentForMonthMonth!=null,
                x => x.AttendentForMonthMonth == input.AttendentForMonthMonth)
            .OrderBy(x=>NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);
        
        
        var queryResult = await AsyncExecuter.ToListAsync(listSalary);

        var totalCount = await Repository.GetCountAsync();
        return new PagedResultDto<SalaryDto>(
            totalCount,
            queryResult
        );*/
    }
    
    [Authorize(HRMappPermissions.Employee.Default)]
    public async Task<ListResultDto<SelectResultDto>> GetListEmployeeAsync()
    {
        var obj = await _employeeRepository.GetListAsync();
        return new ListResultDto<SelectResultDto>(ObjectMapper.Map<List<Employee>, List<SelectResultDto>>(obj));
    }

    public async Task<SalaryDto> GetSalaryDetail(Guid salaryId)
    {
        var query = from salary in (await Repository.GetQueryableAsync())
            join attendentformonth in await _attendentForMonthRepository.GetQueryableAsync() on salary.AttendentForMonthId equals attendentformonth.Id
                into salaryAttendentForMonth
            from salaryAttendentForMonths in salaryAttendentForMonth.DefaultIfEmpty()
            join employee in await _employeeRepository.GetQueryableAsync() on salary.EmployeeId equals employee.Id
                into Salaryemployee
            from employees in Salaryemployee.DefaultIfEmpty()
            where salary.Id == salaryId
            select new SalaryDto()
            {
                Id = salary.Id,
                EmployeeId = salary.EmployeeId,
                EmployeeName = employees.Name,
                AttendentForMonthId = salary.AttendentForMonthId,
                AttendentForMonthMonth = salaryAttendentForMonths.Month,
                TotalSalary = salary.TotalSalary
            };
        var queryResult = await AsyncExecuter.FirstAsync(query);

        return queryResult;
    }

    public async override Task<SalaryDto> CreateAsync(CreateUpdateSalaryDto input)
    {
        
        var employees = await _employeeRepository.GetQueryableAsync();
        var attendances = (from employee in employees join attendent in await _attendentForMonthRepository.GetQueryableAsync()
                on employee.Id equals attendent.EmployeeId where employee.Id == input.EmployeeId && attendent.Month.Month == input.AttendentForMonthMonth.GetValueOrDefault().Month &&
                                                                 attendent.Month.Year == input.AttendentForMonthMonth.GetValueOrDefault().Year
            select attendent.Count).First();
        var attendentformonthId =  (from attendentformonth in await _attendentForMonthRepository.GetQueryableAsync()
            join employee in employees
                on attendentformonth.EmployeeId equals employee.Id
            where attendentformonth.EmployeeId == input.EmployeeId && attendentformonth.Month.Month == input.AttendentForMonthMonth.GetValueOrDefault().Month 
                  && attendentformonth.Month.Year == input.AttendentForMonthMonth.GetValueOrDefault().Year
            select attendentformonth.Id).First();
        
        var contracts = (from employee in employees join contract in await _contractRepository.GetQueryableAsync()
                on employee.Id equals contract.EmployeeId where employee.Id == input.EmployeeId 
            select contract.CoefficientSalary).First();
        // var totalSalary = from salary in await _repository.GetQueryableAsync() join 
        //     attendentForMonth in await _attendentForMonthRepository.GetQueryableAsync() on salary.AttendentForMonthId equals attendentForMonth.Id
        //     where salary.EmployeeId == input.EmployeeId && attendentForMonth.Month.Month == input.AttendentForMonthMonth.
        //     select attendentForMonth.Count
        // return base.CreateAsync(input);

        var TotalSalary = (decimal)attendances * contracts / 4;
          
        var salary = (await _repository.GetQueryableAsync()).Where(x => x.EmployeeId == input.EmployeeId)
            .Where(x => x.AttendentForMonthId == input.AttendentForMonthId).ToList().FirstOrDefault();
        if (salary != null)
        {
            salary.TotalSalary = TotalSalary;
            await _repository.UpdateAsync(salary);
        }
        else
        {
            await _repository.InsertAsync(new Salary(GuidGenerator.Create(), CurrentTenant.Id, input.EmployeeId,
                attendentformonthId, TotalSalary));
        }

        return new SalaryDto();
    }


    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"salary.{nameof(Salary.Id)}";
        }

        // custom contain sorting 
        if (sorting.Contains("employeeName", StringComparison.OrdinalIgnoreCase))
        {
            return "employeeName";
        }
        return $"salary.{sorting}";
    }
}
