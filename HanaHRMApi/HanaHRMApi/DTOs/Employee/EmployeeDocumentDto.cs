using System.ComponentModel.DataAnnotations;

namespace HanaHRMApi.DTOs.Employee;

public class EmployeeDocumentDto
{
    public int? Id { get; set; }
    public string DocumentName { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public DateTime UploadDate { get; set; }
    public string? UploadedFileExtention { get; set; }
    public byte[]? UploadedFile { get; set; }

    public DateTime? SetDate { get; set; }
    public string? CreatedBy { get; set; }
}
