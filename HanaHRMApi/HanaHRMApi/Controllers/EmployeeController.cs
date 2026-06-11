using HanaHRMApi.DTOs.Employee;
using HanaHRMApi.DTOs.Shared;
using HanaHRMApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HanaHRMApi.Controllers;

[ApiController]
[Route("api/employee")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponseDto<List<EmployeeListDto>>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _employeeRepository.GetEmployeeListAsync(cancellationToken);
        return Ok(ApiResponseDto<List<EmployeeListDto>>.SuccessResponse(result));
    }

    [HttpGet("details/{idEmployee}")]
    public async Task<ActionResult<ApiResponseDto<EmployeeDetailDto>>> GetById([FromRoute] int idEmployee, CancellationToken cancellationToken)
    {
        var result = await _employeeRepository.GetEmployeeByIdAsync(idEmployee, cancellationToken);
        if (result is null)
        {
            return NotFound(ApiResponseDto<EmployeeDetailDto>.Failure("Employee not found."));
        }

        return Ok(ApiResponseDto<EmployeeDetailDto>.SuccessResponse(result));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponseDto<int>>> Create([FromBody] EmployeeDto dto, CancellationToken cancellationToken)
    {
        var newEmployeeId = await _employeeRepository.CreateEmployeeAsync(dto, cancellationToken);
        var response = ApiResponseDto<int>.SuccessResponse(newEmployeeId, "Employee created successfully.");

        return CreatedAtAction(nameof(GetById), new { idEmployee = newEmployeeId }, response);
    }

    [HttpPut("{idEmployee}")]
    public async Task<ActionResult<ApiResponseDto<object>>> Update([FromRoute] int idEmployee, [FromBody] EmployeeDto dto, CancellationToken cancellationToken)
    {

        if (dto.Id != idEmployee)
        {
            return BadRequest(ApiResponseDto<object>.Failure("Request identifier mismatch."));
        }

        try
        {
            await _employeeRepository.UpdateEmployeeAsync(dto, cancellationToken);
            return Ok(ApiResponseDto<object>.SuccessResponse(new { }, "Employee updated successfully."));
        }
        catch (KeyNotFoundException)
        {
            return NotFound(ApiResponseDto<object>.Failure("Employee not found."));
        }
    }


    [HttpDelete("{idEmployee}")]
    public async Task<ActionResult<ApiResponseDto<object>>> Delete([FromRoute] int idEmployee, CancellationToken cancellationToken)
    {
        await _employeeRepository.DeleteEmployeeAsync(idEmployee, cancellationToken);
        return Ok(ApiResponseDto<object>.SuccessResponse(new { }, "Employee deleted successfully."));
    }
}
