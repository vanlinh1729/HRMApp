using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Departments;
using HRMapp.Departments.Dtos;

namespace HRMapp.Web.Pages.Departments.Department;

public class CreateModalModel : CreateOrEditModalModel
{

    private readonly IDepartmentAppService _service;

    public CreateModalModel(IDepartmentAppService service): base(service)
    {
        _service = service;
    }
}