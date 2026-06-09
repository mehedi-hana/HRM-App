using System.ComponentModel.DataAnnotations;

namespace HanaHRMApi.DTOs.Employee;

public class EmployeeFamilyInfoDto
{
    public int? Id { get; set; }

    [Required]
    public int IdGender { get; set; }

    [Required]
    public int IdRelationship { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }

    [StringLength(50)]
    public string? ContactNo { get; set; }

    [StringLength(500)]
    public string? CurrentAddress { get; set; }

    [StringLength(500)]
    public string? PermanentAddress { get; set; }

    public DateTime? SetDate { get; set; }
    public string? CreatedBy { get; set; }
}
