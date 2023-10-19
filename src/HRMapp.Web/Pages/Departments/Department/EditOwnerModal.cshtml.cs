using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Departments;
using HRMapp.Departments.Dtos;
using HRMapp.Web.Pages.Departments.Department.ViewModels;

namespace HRMapp.Web.Pages.Departments.Department;

public class EditOwnerModal : CreateOrEditModalModel
{
    private readonly IDepartmentAppService _service;

    public EditOwnerModal(IDepartmentAppService service): base(service)
    {
        _service = service;
    }
}