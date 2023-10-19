using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Departments;
using HRMapp.Departments.Dtos;
using HRMapp.Web.Pages.Departments.Department.ViewModels;

namespace HRMapp.Web.Pages.Departments.Department;

public class EditModalModel : CreateOrEditModalModel
{
    private readonly IDepartmentAppService _service;

    public EditModalModel(IDepartmentAppService service): base(service)
    {
        _service = service;
    }
}
