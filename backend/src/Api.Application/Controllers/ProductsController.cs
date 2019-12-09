using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos.Product;
using Api.Domain.Interfaces.Services.Product;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
  [Route("/api/v1/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    private IProductService _service;
    public ProductsController(IProductService service)
    {
      _service = service;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        return Ok(await _service.GetAll());
      }
      catch (Exception e)
      {
        var errorCode = (int)HttpStatusCode.InternalServerError;
        var msg = e.Message;

        if (msg.StartsWith("BadRequest:"))
        {
          errorCode = (int)HttpStatusCode.BadRequest;
          msg = msg.Split(": ")[1];
        }

        return StatusCode(errorCode, new { error = msg });
      }
    }

    [HttpGet]
    [Route("{id}", Name = "GetById")]
    public async Task<ActionResult> GetById(Guid id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        return Ok(await _service.GetById(id));
      }
      catch (Exception e)
      {
        var errorCode = (int)HttpStatusCode.InternalServerError;
        var msg = e.Message;

        if (msg.StartsWith("BadRequest:"))
        {
          errorCode = (int)HttpStatusCode.BadRequest;
          msg = msg.Split(": ")[1];
        }

        return StatusCode(errorCode, new { error = msg });
      }
    }

    [HttpPost]
    public async Task<ActionResult> Store([FromBody] ProductDtoCreate product)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        var result = await _service.Post(product);

        if (result != null)
        {
          return Created(new Uri(Url.Link("GetById", new { id = result.Id })), result);
        }
        else
        {
          return BadRequest();
        }
      }
      catch (Exception e)
      {
        var errorCode = (int)HttpStatusCode.InternalServerError;
        var msg = e.Message;

        if (msg.StartsWith("BadRequest:"))
        {
          errorCode = (int)HttpStatusCode.BadRequest;
          msg = msg.Split(": ")[1];
        }

        return StatusCode(errorCode, new { error = msg });
      }
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] ProductDtoUpdate product)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        var result = await _service.Put(product);

        if (result != null)
        {
          return Ok(result);
        }
        else
        {
          return BadRequest();
        }
      }
      catch (Exception e)
      {
        var errorCode = (int)HttpStatusCode.InternalServerError;
        var msg = e.Message;

        if (msg.StartsWith("BadRequest:"))
        {
          errorCode = (int)HttpStatusCode.BadRequest;
          msg = msg.Split(": ")[1];
        }

        return StatusCode(errorCode, new { error = msg });
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        return Ok(await _service.Delete(id));
      }
      catch (Exception e)
      {
        var errorCode = (int)HttpStatusCode.InternalServerError;
        var msg = e.Message;

        if (msg.StartsWith("BadRequest:"))
        {
          errorCode = (int)HttpStatusCode.BadRequest;
          msg = msg.Split(": ")[1];
        }

        return StatusCode(errorCode, new { error = msg });
      }
    }
  }
}
