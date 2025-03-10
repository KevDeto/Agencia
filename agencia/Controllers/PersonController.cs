using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using agencia.Context;
using agencia.Models.Entity;
using agencia.Models.Dto;
using AutoMapper;
using agencia.Models.Repository;
using agencia.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using agencia.Services.Interfaces;

namespace agencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        // POST: api/Person
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonDTO>> CreatePerson([FromBody] PersonDTO personRequest)
        {
            try
            {
                var createdPerson = await _personService.CreateAsync(personRequest);
                return CreatedAtAction(nameof(GetByIdPerson), new { id = createdPerson.Id }, createdPerson);
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

        // GET: api/Person/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<PersonDTO>> GetByIdPerson([FromRoute] long Id)
        {
            try
            {
                var getPerson = await _personService.GetByIdAsync(Id);
                return Ok(getPerson);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: ${ex.Message}");
            }


        }

        // PUT: api/Person/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdatePerson([FromRoute] long Id, [FromBody] PersonDTO personRequest)
        {
            if (Id != personRequest.Id)
            {
                return BadRequest("ID mismatch.");
            }
            try
            {
                await _personService.UpdateAsync(personRequest);
                return NoContent();
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
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        // DELETE: api/Person/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] long Id)
        {
            try
            {
                await _personService.DeleteAsync(Id);
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

        // GET: api/Person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetAllPersons()
        {
            try
            {
                var personsResponse = await _personService.GetAllAsync();
                return Ok(personsResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
