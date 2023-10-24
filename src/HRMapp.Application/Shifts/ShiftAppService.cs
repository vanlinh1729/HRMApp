using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using HRMapp.Permissions;
using HRMapp.Shifts.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Shifts;


public class ShiftAppService : CrudAppService<Shift, ShiftDto, Guid, ShiftGetListInput, CreateUpdateShiftDto, CreateUpdateShiftDto>,
    IShiftAppService
{
    protected override string GetPolicyName { get; set; } = HRMappPermissions.Shift.Default;
    protected override string GetListPolicyName { get; set; } = HRMappPermissions.Shift.Default;
    protected override string CreatePolicyName { get; set; } = HRMappPermissions.Shift.Create;
    protected override string UpdatePolicyName { get; set; } = HRMappPermissions.Shift.Update;
    protected override string DeletePolicyName { get; set; } = HRMappPermissions.Shift.Delete;

    private readonly IShiftRepository _repository;
    public ShiftAppService(IShiftRepository repository
    ) : base(repository)
    {   
        _repository = repository;
    }

    //    // protected override async Task<IQueryable<Shift>> CreateFilteredQueryAsync(ShiftGetListInput input)
    // {
    //     // TODO: AbpHelper generated
    //     return (await base.CreateFilteredQueryAsync(input))
    //    //    //    //    //         .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
    //    //    //    //    //         .WhereIf(input.Start != null, x => x.Start == input.Start)
    //    //    //    //    //         .WhereIf(input.End != null, x => x.End == input.End)
    //    //    //         ;
    // }
    //
    [Authorize(HRMappPermissions.Shift.Default)]
    public override async Task<PagedResultDto<ShiftDto>> GetListAsync(ShiftGetListInput input)
    {
        var queryable = await Repository.GetQueryableAsync();
        var query = from shift in queryable
        select new 
            { shift
            };
        query = query
        .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.shift.Name.ToLower().Contains(input.Name.ToLower()))
        .WhereIf(input.Start != null, x => x.shift.Start == input.Start)
        .WhereIf(input.End != null, x => x.shift.End == input.End)
            .OrderBy(NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);
        var queryResult = await AsyncExecuter.ToListAsync(query);
        var ShiftDtos = queryResult.Select(x =>
        {
            var ShiftDtoDto = ObjectMapper.Map<Shift, ShiftDto>(x.shift);
            return ShiftDtoDto;
        }).ToList();
        var totalCount = await Repository.GetCountAsync();
        return new PagedResultDto<ShiftDto>(
            totalCount,
                ShiftDtos
        );
        
    }


    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"shift.{nameof(Shift.Id)}";
        }
        // custom contain sorting 
        return $"shift.{sorting}";
    }


[Authorize(HRMappPermissions.Shift.Update)]
public override Task<ShiftDto> GetAsync(Guid id)
{
    return base.GetAsync(id);
}

}