using System.ComponentModel.DataAnnotations;

namespace HanaHRMApi.DTOs.Employee;

public class EmployeeEducationInfoDto
{
    public int? Id { get; set; }

    [Required]
    public int IdEducationLevel { get; set; }

    [Required]
    public int IdEducationExamination { get; set; }

    [Required]
    public int IdEducationResult { get; set; }

    [Required]
    [StringLength(50)]
    public string Major { get; set; } = null!;

    [Required]
    [Range(1900, 9999)]
    public decimal PassingYear { get; set; }

    [Required]
    [StringLength(250)]
    public string InstituteName { get; set; } = null!;

    public decimal? Cgpa { get; set; }
    public decimal? ExamScale { get; set; }
    public decimal? Marks { get; set; }
    public bool IsForeignInstitute { get; set; }
    public decimal? Duration { get; set; }
    public string? Achievement { get; set; }
    public DateTime? SetDate { get; set; }
    public string? CreatedBy { get; set; }
}
