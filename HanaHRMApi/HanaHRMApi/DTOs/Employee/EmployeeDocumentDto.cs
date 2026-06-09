using System.ComponentModel.DataAnnotations;

namespace HanaHRMApi.DTOs.Employee;

public class EmployeeDocumentDto
{
    public int? Id { get; set; }

    [Required]
    [StringLength(200)]
    public string DocumentName { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string FileName { get; set; } = null!;

    [Required]
    public DateTime UploadDate { get; set; }

    [StringLength(10)]
    public string? UploadedFileExtention { get; set; }

    [Required]
    public byte[] UploadedFile { get; set; } = null!;

    public DateTime? SetDate { get; set; }
    public string? CreatedBy { get; set; }
}
