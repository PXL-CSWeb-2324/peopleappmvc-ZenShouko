using Microsoft.AspNetCore.Mvc;
using PeopleApp.ClassLib.Models;
using PeopleApp.Mvc.Services.Interfaces;

namespace PeopleApp.Mvc.Controllers;

public class DepartmentController : Controller
{
    private IDepartmentRepository _repo;

    public DepartmentController(IDepartmentRepository repo)
    {
        _repo = repo;
    }

    public async Task<IActionResult> IndexAsync()
    {
        //Get all departments
        var result = await _repo.GetAsync();
        if (result.Succeeded)
        {
            return View(result.Entities);
        }

        //If there is an error, return an empty list
        return View(Enumerable.Empty<Department>());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Department department)
    {
        if (ModelState.IsValid)
        {
            var result = await _repo.AddAsync(department);
            if (result.Succeeded)
                return RedirectToAction("Index");
            ModelState.AddModelError("", result.Error);
        }
        return View(department);
    }
}