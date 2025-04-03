using agencia.Models.Dto;
using agencia.Models.Repository.Interfaces;
using agencia.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace agencia.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        public IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceDTO>> CreateService([FromBody] ServiceDTO serviceRequest)
        {
            try
            {
                var createService = await _serviceService.CreateAsync(serviceRequest);
                return CreatedAtAction(nameof(GetByIdService), new { id = createService.Id }, createService);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdService([FromRoute] long Id)
        {
            try
            {
                var getService = await _serviceService.GetByIdAsync(Id);
                return Ok(getService);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: ${ex.Message}");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteService([FromRoute] long Id)
        {
            try
            {
                await _serviceService.DeleteAsync(Id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: ${ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateService([FromRoute] long Id, [FromBody] ServiceDTO serviceRequest)
        {
            try
            {
                await _serviceService.UpdateAsync(Id, serviceRequest);
                return Ok(serviceRequest);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(500, $"A concurrency error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: ${ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            try
            {
                var getAllServices = await _serviceService.GetAllAsync();
                return Ok(getAllServices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: ${ex.Message}");
            }
        }
    }
}
