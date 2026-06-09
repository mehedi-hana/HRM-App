using HanaHRMApi.DTOs.Employee;

namespace HanaHRMApi.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<List<EmployeeListDto>> GetEmployeeListAsync(CancellationToken cancellationToken);
    Task<EmployeeDetailDto?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> CreateEmployeeAsync(EmployeeCreateDto dto, CancellationToken cancellationToken);
    Task<int> UpdateEmployeeAsync(EmployeeUpdateDto dto, CancellationToken cancellationToken);
    Task DeleteEmployeeAsync(int id, CancellationToken cancellationToken);
}
