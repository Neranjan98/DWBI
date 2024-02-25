using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualizationService.Factories.Base;
using VirtualizationService.Persistence;
using VirtualizationService.Services;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace VirtualizationService.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DataVirtualizationController : ControllerBase
    {
        private readonly IDataVirtualizationService _service;

        public DataVirtualizationController(IDataVirtualizationService service)
        {
            _service = service;
        }

        [HttpGet(Name = "GetConnections")]
        public async Task<IActionResult> GetConnections(CancellationToken ct)
        {
            try
            {
                var response = await _service.GetAllConnections(ct);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var exception = ex.Message;
                return StatusCode(500, "Error occured during Processing your request.");
            }
        }

        [HttpGet("{type}", Name = "GetConnectionsByType")]
        public async Task<IActionResult> GetConnectionsByType(string type, CancellationToken ct)
        {
            if (String.IsNullOrWhiteSpace(type))
            {
                return BadRequest("Please provide a type");
            }

            try
            {
                var connection = await _service.GetAllConnectionByType(type, ct);

                return Ok(connection);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error occured during Processing your request.");
            }

        }
    }
}
