using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRMapp.Employees;
using HRMapp.Shifts;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace HRMapp.Attendents;

public class AttendentDataSeeder
    : IDataSeedContributor, ITransientDependency
{
    private readonly IEmployeeRepository _employeeRepository;

    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly IShiftRepository _shiftRepository;
    private readonly IAttendentRepository _attendentRepository;
    private readonly IAttendentForMonthRepository _attendentForMonthRepository;
    private readonly IAttendentLineRepository _attendentLineRepository;


    public AttendentDataSeeder(
        IEmployeeRepository employeeRepository,
        IGuidGenerator guidGenerator,
        ICurrentTenant currentTenant,
        IShiftRepository shiftRepository,
        IAttendentRepository attendentRepository,
        IAttendentForMonthRepository attendentForMonthRepository,
        IAttendentLineRepository attendentLineRepository
    )
    {
        _attendentForMonthRepository = attendentForMonthRepository;
        _attendentRepository = attendentRepository;
        _attendentLineRepository = attendentLineRepository;
        _shiftRepository = shiftRepository;

        _employeeRepository = employeeRepository;
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await AddAttendentSeeder(context);
        await AddAttendentLineSeeder(context);
        await AddAttendentForMonthSeeder(context);
    }

    public async Task AddAttendentSeeder(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            if (await _attendentRepository.GetCountAsync() > 0)
            {
                return;
            }

            var tenantId = context?.TenantId;
            var employees = await _employeeRepository.GetListAsync();
            var attendents = new List<Attendent>();

            foreach (var employee in employees)
            {
                var attendent1 = new Attendent(
                    _guidGenerator.Create(),
                    tenantId,
                    new DateTime(2023, 10, 01),
                    employee.Id,
                    0, 0
                );
                var attendent2 = new Attendent(
                    _guidGenerator.Create(),
                    tenantId,
                    new DateTime(2023, 10, 02),
                    employee.Id,
                    0, 0
                );
                var attendent3 = new Attendent(
                    _guidGenerator.Create(),
                    tenantId,
                    new DateTime(2023, 10, 03),
                    employee.Id,
                    0, 0
                );
                attendents.Add(attendent1);
                attendents.Add(attendent2);
                attendents.Add(attendent3);
            }

            await _attendentRepository.InsertManyAsync(attendents);
        }
    }

    public async Task AddAttendentLineSeeder(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            if (await _attendentLineRepository.GetCountAsync() > 0)
            {
                return;
            }

            var tenantId = context?.TenantId;
            var shift = await _shiftRepository.GetListAsync();
            var attendents = await _attendentRepository.GetListAsync();
            var attendentLines = new List<AttendentLine>();

            foreach (var attendent in attendents)
            {
                var attendent1 = new AttendentLine(
                    _guidGenerator.Create(),
                    tenantId,
                    attendent.Id,
                    new DateTime(2023, 10, attendent.Date.Day, 7, 35, 0),
                    TypeLine.CheckIn,
                    shift[0].Id,
                    0, 0
                );
                var attendent2 = new AttendentLine(
                    _guidGenerator.Create(),
                    tenantId,
                    attendent.Id,
                    new DateTime(2023, 10, attendent.Date.Day, 11, 10, 0),
                    TypeLine.CheckOut,
                    shift[0].Id,
                    0, 0
                );
                var attendent3 = new AttendentLine(
                    _guidGenerator.Create(),
                    tenantId,
                    attendent.Id,
                    new DateTime(2023, 10, attendent.Date.Day, 13, 45, 0),
                    TypeLine.CheckIn,
                    shift[1].Id,
                    0, 0
                );
                var attendent4 = new AttendentLine(
                    _guidGenerator.Create(),
                    tenantId,
                    attendent.Id,
                    new DateTime(2023, 10, attendent.Date.Day, 17, 35, 0),
                    TypeLine.CheckOut,
                    shift[1].Id,
                    0, 0
                );

                attendentLines.Add(attendent1);
                attendentLines.Add(attendent2);
                attendentLines.Add(attendent3);
                attendentLines.Add(attendent4);
            }

            await _attendentLineRepository.InsertManyAsync(attendentLines);
        }
    }

    public async Task AddAttendentForMonthSeeder(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            if (await _attendentForMonthRepository.GetCountAsync() > 0)
            {
                return;
            }

            var tenantId = context?.TenantId;
            var employees = await _employeeRepository.GetListAsync();
            var attendentForMonths = new List<AttendentForMonth>();

            foreach (var employee in employees)
            {
                var attendentformonth = new AttendentForMonth(
                    _guidGenerator.Create(),
                    tenantId,
                    employee.Id,
                    new DateTime(2023, 10, 1, 0, 0, 0),
                    12
                );

                attendentForMonths.Add(attendentformonth);
            }

            await _attendentForMonthRepository.InsertManyAsync(attendentForMonths);
        }
    }
}