namespace HanaHRMApi.DTOs.Employee;

public class EmployeeListDto
{
    public int Id { get; set; }
    public string EmployeeName { get; set; } = null!;
    public string? DesignationName { get; set; }
}
