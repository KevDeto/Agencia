using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using agencia.Context;
using agencia.Models.Entity;
using agencia.Services.Interfaces;
using agencia.Models.Dto;
using agencia.Services;

namespace agencia.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult<ClientDTO>> CreateClient([FromBody] ClientDTO clientRequest)
        {
            try
            {
                var createClient = await _clientService.CreateAsync(clientRequest);
                return CreatedAtAction(nameof(GetByIdClient), new { id = createClient.Id }, createClient);
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
        public async Task<IActionResult> GetByIdClient([FromRoute] long Id)
        {
            try
            {
                var getClient = await _clientService.GetByIdAsync(Id);
                return Ok(getClient);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: ${ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateClient([FromRoute] long Id, [FromBody] ClientDTO clientRequest)
        {
            try
            {
                await _clientService.UpdateAsync(Id, clientRequest);
                return Ok(clientRequest);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                return StatusCode(500, $"A concurrency error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: ${ex.Message}");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteClient([FromRoute] long Id)
        {
            try
            {
                await _clientService.DeleteAsync(Id);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetAllClients()
        {
            try
            {
                var clientResponse = await _clientService.GetAllAsync();
                return Ok(clientResponse);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        } 

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

    }
}
