using HRMapp.Employees.Dtos;
using HRMapp.Web.Pages.Employees.Employee.ViewModels;
using HRMapp.Departments.Dtos;
using HRMapp.Web.Pages.Departments.Department.ViewModels;
using HRMapp.Shifts.Dtos;
using HRMapp.Web.Pages.Shifts.Shift.ViewModels;
using HRMapp.Contacts.Dtos;
using HRMapp.Web.Pages.Contacts.Contact.ViewModels;
using HRMapp.Contracts.Dtos;
using HRMapp.Web.Pages.Contracts.Contract.ViewModels;
using HRMapp.Salarys.Dtos;
using HRMapp.Web.Pages.Salarys.Salary.ViewModels;
using HRMapp.Attendents.Dtos;
using HRMapp.Web.Pages.Attendents.Attendent.ViewModels;
using HRMapp.Attendents.Dtos;
using HRMapp.Web.Pages.Attendents.AttendentLine.ViewModels;
using HRMapp.Attendents.Dtos;
using HRMapp.Web.Pages.Attendents.AttendentForMonth.ViewModels;
using HRMapp.Employees.Dtos;
using HRMapp.Web.Pages.Employees.EmployeeHistory.ViewModels;
using AutoMapper;
using HRMapp.Contacts;
using HRMapp.Web.Pages.Departments.Department;
using Volo.Abp.AutoMapper;

namespace HRMapp.Web;

public class HRMappWebAutoMapperProfile : Profile
{
    public HRMappWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<EmployeeDto, CreateEditEmployeeViewModel>();
        CreateMap<CreateEditEmployeeViewModel, CreateUpdateEmployeeDto>();
        CreateMap<DepartmentDto, CreateEditDepartmentViewModel>();
        CreateMap<CreateEditDepartmentViewModel, CreateUpdateDepartmentDto>();
        CreateMap<ShiftDto, CreateEditShiftViewModel>();
        CreateMap<CreateEditShiftViewModel, CreateUpdateShiftDto>();
        CreateMap<ContactDto, CreateEditContactViewModel>();
        CreateMap<CreateEditContactViewModel, CreateUpdateContactDto>();
        CreateMap<ContractDto, CreateEditContractViewModel>();
        CreateMap<CreateEditContractViewModel, CreateUpdateContractDto>();
        CreateMap<SalaryDto, CreateEditSalaryViewModel>();
        CreateMap<CreateEditSalaryViewModel, CreateUpdateSalaryDto>();
        CreateMap<AttendentDto, CreateEditAttendentViewModel>();
        CreateMap<CreateEditAttendentViewModel, CreateUpdateAttendentDto>();
        CreateMap<AttendentLineDto, CreateEditAttendentLineViewModel>();
        CreateMap<CreateEditAttendentLineViewModel, CreateUpdateAttendentLineDto>();
        CreateMap<AttendentForMonthDto, CreateEditAttendentForMonthViewModel>();
        CreateMap<CreateEditAttendentForMonthViewModel, CreateUpdateAttendentForMonthDto>();
        CreateMap<EmployeeHistoryDto, CreateEditEmployeeHistoryViewModel>();
        CreateMap<CreateEditEmployeeHistoryViewModel, CreateUpdateEmployeeHistoryDto>();
        
        
          /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<DepartmentDto, CreateEditDepartmentViewModel>().Ignore(x=>x.employeeId);
        CreateMap<CreateEditDepartmentViewModel, CreateUpdateDepartmentDto>();
        
        CreateMap<EmployeeDto, CreateEditEmployeeViewModel>();
        CreateMap<CreateEditEmployeeViewModel, CreateUpdateEmployeeDto>();
        CreateMap<ShiftDto, CreateEditShiftViewModel>();
        CreateMap<CreateEditShiftViewModel, CreateUpdateShiftDto>();
      

        /*CreateMap<AttendentDto, CreateEditAttendentViewModel>();*//*.Ignore(x=>x.Type).Ignore(x=>x.ShiftId);*/
        CreateMap<CreateEditAttendentViewModel, CreateUpdateAttendentDto>();
       
       
          
        CreateMap<DepartmentChangeOwnerDto,EntityChangeWithUsernameViewModel>();
        CreateMap<EmployeeWithName,EmployeeNameViewModel>();
        CreateMap<CreateUpdateContactDto,Contact>();
        

       
     
        CreateMap<AttendentLineDto, CreateEditAttendentLineViewModel>();
        CreateMap<CreateEditAttendentLineViewModel, CreateUpdateAttendentLineDto>();

        CreateMap<CreateEditDepartmentViewModel, CreateDepartmentAndAddEmployee>();
        CreateMap<CreateManySalaryViewModel , CreateManySalaryDto>();
        CreateMap<CreateManyAttendentForMonthViewModel , CreateManyAttendentForMonthDto>();
    }
}
