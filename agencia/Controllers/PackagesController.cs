using agencia.Models.Dto;
using agencia.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace agencia.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackagesController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpPost]
        public async Task<ActionResult<PackageDTO>> CreatePackage([FromBody] PackageDTO packageRequest)
        {
            try
            {
                var createPackage = await _packageService.CreateAsync(packageRequest);
                return CreatedAtAction(nameof(GetByIdPackage), new { id = createPackage.Id }, createPackage);
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
        public async Task<IActionResult> GetByIdPackage([FromRoute] long Id)
        {
            try
            {
                var getPackage = await _packageService.GetByIdAsync(Id);
                return Ok(getPackage);
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
        public async Task<IActionResult> DeletePackage([FromRoute] long Id)
        {
            try
            {
                await _packageService.DeleteAsync(Id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdatePackage([FromRoute] long Id, [FromBody] PackageDTO packageRequest)
        {
            try
            {
                await _packageService.UpdateAsync(Id, packageRequest);
                return Ok(_packageService.GetByIdAsync(Id));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                return StatusCode(500, $"A concurrency error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageDTO>>> GetAllPackage()
        {
            try
            {
                var getAllPackages = await _packageService.GetAllAsync();
                return Ok(getAllPackages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
