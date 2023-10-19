using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Employees;
using HRMapp.Employees.Dtos;
using HRMapp.Web.Pages.Employees.Employee.ViewModels;

namespace HRMapp.Web.Pages.Employees.Employee;

public class EditModalModel : CreateOrEditModalModel
{
    private readonly IEmployeeAppService _service;

    public EditModalModel(IEmployeeAppService service): base(service)
    {
        _service = service;
    }
}