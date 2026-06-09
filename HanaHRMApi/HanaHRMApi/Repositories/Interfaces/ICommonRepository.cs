using HanaHRMApi.DTOs.Common;

namespace HanaHRMApi.Repositories.Interfaces;

public interface ICommonRepository
{
    Task<List<DropdownItemDto>> GetDepartmentsAsync(CancellationToken cancellationToken);
    Task<List<DropdownItemDto>> GetDesignationsAsync(CancellationToken cancellationToken);
    Task<List<DropdownItemDto>> GetGendersAsync(CancellationToken cancellationToken);
    Task<List<DropdownItemDto>> GetJobTypesAsync(CancellationToken cancellationToken);
    Task<List<DropdownItemDto>> GetEmployeeTypesAsync(CancellationToken cancellationToken);
    Task<List<DropdownItemDto>> GetMaritalStatusesAsync(CancellationToken cancellationToken);
    Task<List<DropdownItemDto>> GetReligionsAsync(CancellationToken cancellationToken);
    Task<List<DropdownItemDto>> GetSectionsAsync(CancellationToken cancellationToken);
    Task<List<DropdownItemDto>> GetWeekOffsAsync(CancellationToken cancellationToken);
    Task<List<DropdownItemDto>> GetRelationshipsAsync(CancellationToken cancellationToken);
    Task<List<DropdownItemDto>> GetEducationLevelsAsync(CancellationToken cancellationToken);
    Task<List<DropdownItemDto>> GetEducationExaminationsAsync(CancellationToken cancellationToken);
    Task<List<DropdownItemDto>> GetEducationResultsAsync(CancellationToken cancellationToken);
}
