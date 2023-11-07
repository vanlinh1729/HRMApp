using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Departments;
using HRMapp.Departments.Dtos;
using HRMapp.Web.Pages.Departments.Department.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.ObjectMapping;

namespace HRMapp.Web.Pages.Departments.Department;

public class EditOwnerModal : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    
    [BindProperty]
    public CreateEditDepartmentViewModel ViewModel { get; set; }
    public List<SelectListItem> Owners { get; set; }
    public List<SelectListItem> Parents { get; set; }

    private readonly IDepartmentAppService _service;

    public EditOwnerModal(IDepartmentAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<DepartmentDto,CreateEditDepartmentViewModel>(dto);
        var owners = await _service.GetListOwnerAsync();
        Owners = owners.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
        var parents = await _service.GetListParentAsync();
        Parents = parents.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
       
    }

    public virtual async Task OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditDepartmentViewModel, CreateDepartmentAndAddEmployee>(ViewModel);
        await _service.UpdateAsync(Id, dto);
        
    }

}