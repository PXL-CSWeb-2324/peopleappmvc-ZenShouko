using PeopleApp.ClassLib.Models;
using PeopleApp.Mvc.Helpers;
using PeopleApp.Mvc.Services.Interfaces;

namespace PeopleApp.Mvc.Services;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly IHttpClientFactory _httpClient;

    public DepartmentRepository(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResult<Department>> GetAsync()
    {
        var result = new ApiResult<Department>();
        var client = _httpClient.CreateClient(ApiHelper.ClientName);
        HttpResponseMessage response = await client.GetAsync("department");

        if (response.IsSuccessStatusCode)
        {
            await response.Content.ReadAsAsync<IEnumerable<Department>>();
            result.Succeeded = true;
        }

        return result;
    }

    public async Task<ApiResult<Department>> AddAsync(Department department)
    {
        var result = new ApiResult<Department>();
        var client = _httpClient.CreateClient(ApiHelper.ClientName);
        HttpResponseMessage response = client.PostAsJsonAsync("department", department).Result;

        if (response.IsSuccessStatusCode)
            result.Succeeded = true;
        else
            result.Failed("Error saving department");

        return result;
    }

    public ApiResult<Department> GetById(long id)
    {
        throw new NotImplementedException();
    }
}