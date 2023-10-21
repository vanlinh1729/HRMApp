using HRMapp.Employees;
using HRMapp.Employees.Dtos;
using HRMapp.Departments;
using HRMapp.Departments.Dtos;
using HRMapp.Shifts;
using HRMapp.Shifts.Dtos;
using HRMapp.Contacts;
using HRMapp.Contacts.Dtos;
using HRMapp.Contracts;
using HRMapp.Contracts.Dtos;
using HRMapp.Salarys;
using HRMapp.Salarys.Dtos;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using AutoMapper;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;

namespace HRMapp;

public class HRMappApplicationAutoMapperProfile : Profile
{
    public HRMappApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
       CreateMap<CreateUpdateDepartmentDto, Department>(MemberList.Source);

        CreateMap<CreateUpdateEmployeeDto, Employee>(MemberList.Source);
        
        //Employee
        CreateMap<Contact, SelectResultDto>();
        CreateMap<Department, SelectResultDto>();
        
        CreateMap<Shift, ShiftDto>();
        CreateMap<CreateUpdateShiftDto, Shift>(MemberList.Source);
     
        CreateMap<Shift, SelectResultDto>();
     
        CreateMap<Contact, SelectResultDto>();
        CreateMap<Employee, SelectResultDto>();
        CreateMap<CreateUpdateDepartmentDto, Department>(MemberList.Source);
        CreateMap<CreateUpdateEmployeeDto, Employee>(MemberList.Source);
        CreateMap<Department, DepartmentDto>().Ignore(x => x.ParentName).Ignore(x => x.OwnerName).Ignore(x=>x.Count);
      
        CreateMap<CreateUpdateDepartmentDto, Department>(MemberList.Source);
      
      
        /*
        CreateMap<CreateUpdateSalaryForMonthDto, SalaryForMonth>();
        */
      
        
        CreateMap<Employee, SelectResultDto>();
        /*
        CreateMap<EntityChangeWithUsername ,EntityChangeWithUsernameDto>().Ignore(x=>x.Id);
    */
      
        CreateMap<CreateUpdateAttendentDto, Attendent>(MemberList.Source);
     
        CreateMap<Employee, SelectResultDto>();
        CreateMap<Employee,EmployeeDto>();
        
        CreateMap<CreateUpdateAttendentLineDto, AttendentLine>(MemberList.Source);
        CreateMap<Attendent, SelectResultDto>().Ignore(x=>x.Name);
        CreateMap<Shift, SelectResultDto>();
        CreateMap<IdentityUser, SelectResultDto>();

        CreateMap<Contact, ContactDto>();
    }
}
