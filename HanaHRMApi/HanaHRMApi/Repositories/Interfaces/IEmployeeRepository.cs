using HanaHRMApi.DTOs.Employee;

namespace HanaHRMApi.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<List<EmployeeListDto>> GetEmployeeListAsync(CancellationToken cancellationToken);
    Task<EmployeeDetailDto?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> CreateEmployeeAsync(EmployeeDto dto, CancellationToken cancellationToken);
    Task<int> UpdateEmployeeAsync(EmployeeDto dto, CancellationToken cancellationToken);
    Task DeleteEmployeeAsync(int id, CancellationToken cancellationToken);
}
