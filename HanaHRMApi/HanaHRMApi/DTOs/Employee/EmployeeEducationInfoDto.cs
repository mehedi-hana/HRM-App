using System.ComponentModel.DataAnnotations;

namespace HanaHRMApi.DTOs.Employee;

public class EmployeeEducationInfoDto
{
    public int? Id { get; set; }
    public int IdEducationLevel { get; set; }
    public int IdEducationExamination { get; set; }
    public int IdEducationResult { get; set; }
    public string Major { get; set; } = null!;
    public decimal PassingYear { get; set; }
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
