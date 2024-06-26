using Microsoft.AspNetCore.Mvc;
using PeopleApp.Api.Attributes;
using PeopleApp.Api.Services.Interfaces;
using PeopleApp.Api.ViewModels;
using PeopleApp.ClassLib.Models;

namespace PeopleApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiKey]
public class PersonKeyController : Controller
{
    IPersonService _service;

    public PersonKeyController(IPersonService service)
    {
        _service = service;
    }

    #region Get

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var result = _service.Get();
            if (result.Succeeded)
            {
                return Ok(result.Entities);
            }

            return BadRequest(result.Error);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetDetails(long id)
    {
        try
        {
            var result = _service.GetById(id);
            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Error);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    #endregion

    #region Post

    [HttpPost]
    public IActionResult Add(PersonViewModel model)
    {
        try
        {
            var result = _service.Add(model);
            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Error);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    #endregion

    #region Update

    [HttpPut]
    public IActionResult Update(Person model)
    {
        try
        {
            var result = _service.Update(model);
            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Error);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    #endregion

    #region Delete

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        try
        {
            var result = _service.DeleteById(id);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Error);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    #endregion
}