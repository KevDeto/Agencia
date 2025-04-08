using agencia.Models.Dto;
using agencia.Models.Repository.Interfaces;
using agencia.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace agencia.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        public readonly ISaleService _saleService;
        public readonly IMapper _mapper;

        public SalesController(ISaleService saleService, IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<SaleDTO>> CreateSale([FromBody] SaleDTO saleRequest)
        {
            try
            {
                var createSale = await _saleService.CreateAsync(saleRequest);
                return CreatedAtAction(nameof(GetByIdSale), new { id = createSale.Id }, createSale);
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
        public async Task<IActionResult> GetByIdSale([FromRoute] long Id)
        {
            try
            {
                var getSale = await _saleService.GetByIdAsync(Id);
                return Ok(getSale);
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
        public async Task<IActionResult> DeleteSale([FromRoute] long Id)
        {
            try
            {
                await _saleService.DeleteAsync(Id);
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
        public async Task<IActionResult> UpdateSale([FromRoute] long Id, [FromBody] SaleDTO saleRequest)
        {
            try
            {
                await _saleService.UpdateAsync(Id, saleRequest);
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

        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            try
            {
                var getAllSales = await _saleService.GetAllAsync();
                return Ok(getAllSales);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: ${ex.Message}");
            }
        }
    }
}
