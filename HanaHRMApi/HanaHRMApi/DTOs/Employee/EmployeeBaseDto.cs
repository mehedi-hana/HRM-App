using System.ComponentModel.DataAnnotations;

namespace HanaHRMApi.DTOs.Employee
{
    public class EmployeeBaseDto
    {
        [Required]
        public int IdClient { get; set; }

        [StringLength(250)]
        public string? EmployeeName { get; set; }

        [StringLength(250)]
        public string? EmployeeNameBangla { get; set; }

        public byte[]? EmployeeImage { get; set; }

        [StringLength(250)]
        public string? FatherName { get; set; }

        [StringLength(250)]
        public string? MotherName { get; set; }

        public int? IdReportingManager { get; set; }
        public int? IdJobType { get; set; }
        public int? IdEmployeeType { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int? IdGender { get; set; }
        public int? IdReligion { get; set; }

        [Required]
        public int IdDepartment { get; set; }

        [Required]
        public int IdSection { get; set; }

        public int? IdDesignation { get; set; }
        public bool HasOvertime { get; set; }
        public bool HasAttendenceBonus { get; set; }
        public int? IdWeekOff { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }

        [StringLength(250)]
        public string? PresentAddress { get; set; }

        [StringLength(30)]
        public string? NationalIdentificationNumber { get; set; }

        [StringLength(250)]
        public string? ContactNo { get; set; }

        public int? IdMaritalStatus { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? SetDate { get; set; }

        [StringLength(50)]
        public string? CreatedBy { get; set; }

        public List<EmployeeFamilyInfoDto> EmployeeFamilyInfos { get; set; } = new();
        public List<EmployeeEducationInfoDto> EmployeeEducationInfos { get; set; } = new();
        public List<EmployeeDocumentDto> EmployeeDocuments { get; set; } = new();
        public List<EmployeeProfessionalCertificationDto> EmployeeProfessionalCertifications { get; set; } = new();
    }
}


