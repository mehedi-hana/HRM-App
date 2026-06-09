using HanaHRMApi.DTOs.Common;
using HanaHRMApi.DTOs.Shared;
using HanaHRMApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HanaHRMApi.Controllers;

[ApiController]
[Route("api/common")]
public class CommonController : ControllerBase
{
    private readonly ICommonRepository _commonRepository;

    public CommonController(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    [HttpGet("departments")]
    public async Task<ActionResult<ApiResponseDto<List<DropdownItemDto>>>> GetDepartments(CancellationToken cancellationToken)
    {
        var result = await _commonRepository.GetDepartmentsAsync(cancellationToken);
        return Ok(ApiResponseDto<List<DropdownItemDto>>.SuccessResponse(result));
    }

    [HttpGet("designations")]
    public async Task<ActionResult<ApiResponseDto<List<DropdownItemDto>>>> GetDesignations(CancellationToken cancellationToken)
    {
        var result = await _commonRepository.GetDesignationsAsync(cancellationToken);
        return Ok(ApiResponseDto<List<DropdownItemDto>>.SuccessResponse(result));
    }

    [HttpGet("genders")]
    public async Task<ActionResult<ApiResponseDto<List<DropdownItemDto>>>> GetGenders(CancellationToken cancellationToken)
    {
        var result = await _commonRepository.GetGendersAsync(cancellationToken);
        return Ok(ApiResponseDto<List<DropdownItemDto>>.SuccessResponse(result));
    }

    [HttpGet("jobtypes")]
    public async Task<ActionResult<ApiResponseDto<List<DropdownItemDto>>>> GetJobTypes(CancellationToken cancellationToken)
    {
        var result = await _commonRepository.GetJobTypesAsync(cancellationToken);
        return Ok(ApiResponseDto<List<DropdownItemDto>>.SuccessResponse(result));
    }

    [HttpGet("employeetypes")]
    public async Task<ActionResult<ApiResponseDto<List<DropdownItemDto>>>> GetEmployeeTypes(CancellationToken cancellationToken)
    {
        var result = await _commonRepository.GetEmployeeTypesAsync(cancellationToken);
        return Ok(ApiResponseDto<List<DropdownItemDto>>.SuccessResponse(result));
    }

    [HttpGet("maritalstatuses")]
    public async Task<ActionResult<ApiResponseDto<List<DropdownItemDto>>>> GetMaritalStatuses(CancellationToken cancellationToken)
    {
        var result = await _commonRepository.GetMaritalStatusesAsync(cancellationToken);
        return Ok(ApiResponseDto<List<DropdownItemDto>>.SuccessResponse(result));
    }

    [HttpGet("religions")]
    public async Task<ActionResult<ApiResponseDto<List<DropdownItemDto>>>> GetReligions(CancellationToken cancellationToken)
    {
        var result = await _commonRepository.GetReligionsAsync(cancellationToken);
        return Ok(ApiResponseDto<List<DropdownItemDto>>.SuccessResponse(result));
    }

    [HttpGet("sections")]
    public async Task<ActionResult<ApiResponseDto<List<DropdownItemDto>>>> GetSections(CancellationToken cancellationToken)
    {
        var result = await _commonRepository.GetSectionsAsync(cancellationToken);
        return Ok(ApiResponseDto<List<DropdownItemDto>>.SuccessResponse(result));
    }

    [HttpGet("weekoffs")]
    public async Task<ActionResult<ApiResponseDto<List<DropdownItemDto>>>> GetWeekOffs(CancellationToken cancellationToken)
    {
        var result = await _commonRepository.GetWeekOffsAsync(cancellationToken);
        return Ok(ApiResponseDto<List<DropdownItemDto>>.SuccessResponse(result));
    }

    [HttpGet("relationships")]
    public async Task<ActionResult<ApiResponseDto<List<DropdownItemDto>>>> GetRelationships(CancellationToken cancellationToken)
    {
        var result = await _commonRepository.GetRelationshipsAsync(cancellationToken);
        return Ok(ApiResponseDto<List<DropdownItemDto>>.SuccessResponse(result));
    }

    [HttpGet("educationlevels")]
    public async Task<ActionResult<ApiResponseDto<List<DropdownItemDto>>>> GetEducationLevels(CancellationToken cancellationToken)
    {
        var result = await _commonRepository.GetEducationLevelsAsync(cancellationToken);
        return Ok(ApiResponseDto<List<DropdownItemDto>>.SuccessResponse(result));
    }

    [HttpGet("educationexaminations")]
    public async Task<ActionResult<ApiResponseDto<List<DropdownItemDto>>>> GetEducationExaminations(CancellationToken cancellationToken)
    {
        var result = await _commonRepository.GetEducationExaminationsAsync(cancellationToken);
        return Ok(ApiResponseDto<List<DropdownItemDto>>.SuccessResponse(result));
    }

    [HttpGet("educationresults")]
    public async Task<ActionResult<ApiResponseDto<List<DropdownItemDto>>>> GetEducationResults(CancellationToken cancellationToken)
    {
        var result = await _commonRepository.GetEducationResultsAsync(cancellationToken);
        return Ok(ApiResponseDto<List<DropdownItemDto>>.SuccessResponse(result));
    }
}
