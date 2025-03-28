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

namespace agencia.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> CreateEmployee([FromBody] EmployeeDTO employeeRequest)
        {
            try
            {
                var createEmployee = await _employeeService.CreateAsync(employeeRequest);
                return CreatedAtAction(nameof(GetByIdEmployee), new { id = createEmployee.Id }, createEmployee);
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
        public async Task<IActionResult> GetByIdEmployee([FromRoute] long Id)
        {
            try
            {
                var getEmployee = await _employeeService.GetByIdAsync(Id);
                return Ok(getEmployee);
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
        public async Task<IActionResult> UpdateEmployee([FromRoute] long Id, [FromBody] EmployeeDTO employeeRequest)
        {
            try
            {
                await _employeeService.UpdateAsync(Id, employeeRequest);
                return Ok(employeeRequest);
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

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] long Id)
        {
            try
            {
                await _employeeService.DeleteAsync(Id);
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees()
        {
            try
            {
                var getEmployees = await _employeeService.GetAllAsync();
                return Ok(getEmployees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        } 
            // POST: Employees/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            /*        [HttpPost]
                    [ValidateAntiForgeryToken]
                    public async Task<IActionResult> Create([Bind("position,salary,Id,name,lastName,dni,email,telephone,adress,nacionality,birthDate")] Employee employee)
                    {
                        if (ModelState.IsValid)
                        {
                            _context.Add(employee);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        return View(employee);
                    }

                    // POST: Employees/Edit/5
                    // To protect from overposting attacks, enable the specific properties you want to bind to.
                    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                    [HttpPost]
                    [ValidateAntiForgeryToken]
                    public async Task<IActionResult> Edit(long id, [Bind("position,salary,Id,name,lastName,dni,email,telephone,adress,nacionality,birthDate")] Employee employee)
                    {
                        if (id != employee.Id)
                        {
                            return NotFound();
                        }

                        if (ModelState.IsValid)
                        {
                            try
                            {
                                _context.Update(employee);
                                await _context.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException)
                            {
                                if (!EmployeeExists(employee.Id))
                                {
                                    return NotFound();
                                }
                                else
                                {
                                    throw;
                                }
                            }
                            return RedirectToAction(nameof(Index));
                        }
                        return View(employee);
                    }

                    // POST: Employees/Delete/5
                    [HttpPost, ActionName("Delete")]
                    [ValidateAntiForgeryToken]
                    public async Task<IActionResult> DeleteConfirmed(long id)
                    {
                        var employee = await _context.Employees.FindAsync(id);
                        if (employee != null)
                        {
                            _context.Employees.Remove(employee);
                        }

                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    private bool EmployeeExists(long id)
                    {
                        return _context.Employees.Any(e => e.Id == id);
                    }
            */
    }
}

