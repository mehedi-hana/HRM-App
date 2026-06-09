using System.ComponentModel.DataAnnotations;

namespace HanaHRMApi.DTOs.Employee;

public class EmployeeProfessionalCertificationDto
{
    public int? Id { get; set; }

    [Required]
    [StringLength(255)]
    public string CertificationTitle { get; set; } = null!;

    [Required]
    [StringLength(250)]
    public string CertificationInstitute { get; set; } = null!;

    [Required]
    [StringLength(250)]
    public string InstituteLocation { get; set; } = null!;

    [Required]
    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }
    public DateTime? SetDate { get; set; }
    public string? CreatedBy { get; set; }
}
