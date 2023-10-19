using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Departments;
using HRMapp.Departments.Dtos;
using HRMapp.Employees;
using HRMapp.Employees.Dtos;
using HRMapp.Web.Pages.Departments.Department.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMapp.Web.Pages.Departments.Department;

public class ViewModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    public List<EntityChangeWithUsernameViewModel> ViewDepartmentChangeModels { get; set; }
    public List<EmployeeNameViewModel> ViewEmployeeNameDepartmentModels { get; set; }

    [BindProperty]
    public DepartmentDto ViewModel { get; set; }
    public CreateEditDepartmentViewModel ViewModels { get; set; }

    private readonly IDepartmentAppService _service;
    private readonly IEmployeeAppService _employeeAppServiceservice;


    public ViewModalModel(IDepartmentAppService service, IEmployeeAppService employeeAppServiceservice)
    {
       
        _service = service;
        _employeeAppServiceservice = employeeAppServiceservice;
    }

    public virtual async Task OnGetAsync()
    {
        
        var dto = await _service.GetDepartmentDetail(Id);

        if (dto != null)
        {
            // if (dto.OwnerId != null && dto.OwnerId!= null)
            // {
            //     var ower = await _employeeAppServiceservice.GetAsync(dto.OwnerId);
            //     dto.OwnerName = ower.Name;
            // }
            ViewModel = dto;
            /*ViewModels = ObjectMapper.Map<DepartmentDto, CreateEditDepartmentViewModel>(dto);*/
        }

        var listemployeenamedepartment = await _service.GetListEmployeeNameDepartment(Id);
        var departmenteditowner = await _service.GetDepartmentChangeListAsync(Id);
        ViewEmployeeNameDepartmentModels = ObjectMapper.Map<List<EmployeeWithName>,List<EmployeeNameViewModel>>(listemployeenamedepartment);
        ViewDepartmentChangeModels = ObjectMapper.Map<List<DepartmentChangeOwnerDto>,List<EntityChangeWithUsernameViewModel>>(departmenteditowner);
    }
}


public class EntityChangeWithUsernameViewModel
{
    [Display(Name = "UserName")]
    public string UserName { get; set; }
    
    [Display(Name = "NewValue")]
    public string NewValue { get; set; }
    
    [Display(Name = "OriginalValue")]
    public string OriginalValue { get; set; }
    
    [Display(Name = "ChangeTime")]
    public DateTime ChangeTime { get; set; }
}

public class EmployeeNameViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Name")] 
    public string Name { get; set; }
}
