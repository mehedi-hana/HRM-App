using System.ComponentModel.DataAnnotations;

namespace HanaHRMApi.DTOs.Employee;

public class EmployeeProfessionalCertificationDto
{
    public int? Id { get; set; }
    public string CertificationTitle { get; set; } = null!;
    public string CertificationInstitute { get; set; } = null!;
    public string InstituteLocation { get; set; } = null!;
    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }
    public DateTime? SetDate { get; set; }
    public string? CreatedBy { get; set; }
}
