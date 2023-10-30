using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using HRMapp.Attendents;
using HRMapp.Contacts;
using HRMapp.Contracts;
using HRMapp.Departments;
using HRMapp.Salarys;
using HRMapp.Shifts;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace HRMapp.Shifts;

public class ShiftDataSeeder
    : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly IShiftRepository _shiftRepository;

    public ShiftDataSeeder(
        IGuidGenerator guidGenerator,
        ICurrentTenant currentTenant,
        IShiftRepository shiftRepository
    )
    {
        _shiftRepository = shiftRepository;
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await AddShiftSeeder(context);
    }
    public async Task AddShiftSeeder(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            if (await _shiftRepository.GetCountAsync() > 0)
            {
                return;
            }
            var tenantId = context?.TenantId;
            List<Shift> Shifts =new List<Shift>();
            var Shift_morning = new Shift(_guidGenerator.Create(),tenantId, "Sáng", new TimeSpan(8,0,0),new TimeSpan(11,0,0), new TimeSpan(7,30,0),new TimeSpan(11,30,0));
            var Shift_noon = new Shift(_guidGenerator.Create(), tenantId,"Chiều", new TimeSpan(14,0,0),new TimeSpan(17,0,0),new TimeSpan(13,30,0),new TimeSpan(17,30,0));
            Shifts.Add(Shift_morning);
            Shifts.Add(Shift_noon);
            await _shiftRepository.InsertManyAsync(Shifts);
        }
    }

}