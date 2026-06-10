using System.ComponentModel.DataAnnotations;

namespace HanaHRMApi.DTOs.Employee;

public class EmployeeFamilyInfoDto
{
    public int? Id { get; set; }
    public int IdGender { get; set; }
    public int IdRelationship { get; set; }
    public string Name { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }
    public string? ContactNo { get; set; }
    public string? CurrentAddress { get; set; }
    public string? PermanentAddress { get; set; }

    public DateTime? SetDate { get; set; }
    public string? CreatedBy { get; set; }
}
