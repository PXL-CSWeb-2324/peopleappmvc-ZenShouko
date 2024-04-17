using PeopleApp.Mvc.Helpers;
using PeopleApp.ClassLib.Models;

namespace PeopleApp.Mvc.Services.Interfaces;

public interface IDepartmentRepository
{
    Task<ApiResult<Department>> GetAsync();
    ApiResult<Department> GetById(long id);
    Task<ApiResult<Department>> AddAsync();
    
}