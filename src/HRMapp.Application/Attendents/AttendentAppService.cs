using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.Permissions;
using HRMapp.Attendents.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Attendents;


public class AttendentAppService : CrudAppService<Attendent, AttendentDto, Guid, AttendentGetListInput, CreateUpdateAttendentDto, CreateUpdateAttendentDto>,
    IAttendentAppService
{
    protected override string GetPolicyName { get; set; } = HRMappPermissions.Attendent.Default;
    protected override string GetListPolicyName { get; set; } = HRMappPermissions.Attendent.Default;
    protected override string CreatePolicyName { get; set; } = HRMappPermissions.Attendent.Create;
    protected override string UpdatePolicyName { get; set; } = HRMappPermissions.Attendent.Update;
    protected override string DeletePolicyName { get; set; } = HRMappPermissions.Attendent.Delete;

    private readonly IAttendentRepository _repository;

    public AttendentAppService(IAttendentRepository repository) : base(repository)
    {
        _repository = repository;
    }

    // protected override async Task<IQueryable<Attendent>> CreateFilteredQueryAsync(AttendentGetListInput input)
    // {
    //     // TODO: AbpHelper generated
    //     return (await base.CreateFilteredQueryAsync(input))
    //         /*
    //         .WhereIf(input.Date != null, x => x.Date == input.Date)
    //         */
    //         .WhereIf(input.EmployeeId != null, x => x.EmployeeId == input.EmployeeId)
    //         .WhereIf(input.MissingIn != null, x => x.MissingIn == input.MissingIn)
    //         .WhereIf(input.MissingOut != null, x => x.MissingOut == input.MissingOut)
    //         /*
    //         .WhereIf(input.AttendentLines != null, x => x.AttendentLines == input.AttendentLines)
    //         */
    //         ;
    // }
    
    
    
    
}
