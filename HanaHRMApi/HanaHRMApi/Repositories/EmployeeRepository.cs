using HanaHRMApi.DTOs.Employee;
using HanaHRMApi.Models;
using HanaHRMApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HanaHRMApi.Repositories;

public class EmployeeRepository(HanaHrmContext context) : IEmployeeRepository
{
    private readonly HanaHrmContext _context = context;

    public async Task<List<EmployeeListDto>> GetEmployeeListAsync(CancellationToken cancellationToken)
    {
        return await _context.Employees
            .AsNoTracking()
            .OrderByDescending(item => item.Id)
            .Select(item => new EmployeeListDto
            {
                Id = item.Id,
                EmployeeName = item.EmployeeName ?? string.Empty,
                DesignationName = item.Designation != null ? item.Designation.DesignationName : null
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<EmployeeDetailDto?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
            .AsNoTracking()
            .Where(employee => employee.Id == id)
            .Select(employee => new EmployeeDetailDto
            {
                IdClient = employee.IdClient,
                Id = employee.Id,
                EmployeeName = employee.EmployeeName,
                EmployeeNameBangla = employee.EmployeeNameBangla,
                EmployeeImage = employee.EmployeeImage,
                FatherName = employee.FatherName,
                MotherName = employee.MotherName,
                IdReportingManager = employee.IdReportingManager,
                IdJobType = employee.IdJobType,
                IdEmployeeType = employee.IdEmployeeType,
                BirthDate = employee.BirthDate,
                JoiningDate = employee.JoiningDate,
                IdGender = employee.IdGender,
                IdReligion = employee.IdReligion,
                IdDepartment = employee.IdDepartment,
                IdSection = employee.IdSection,
                IdDesignation = employee.IdDesignation,
                HasOvertime = employee.HasOvertime,
                HasAttendenceBonus = employee.HasAttendenceBonus,
                IdWeekOff = employee.IdWeekOff,
                Address = employee.Address,
                PresentAddress = employee.PresentAddress,
                NationalIdentificationNumber = employee.NationalIdentificationNumber,
                ContactNo = employee.ContactNo,
                IdMaritalStatus = employee.IdMaritalStatus,
                IsActive = employee.IsActive,
                SetDate = employee.SetDate,
                CreatedBy = employee.CreatedBy,
                EmployeeDocuments = employee.EmployeeDocuments.Select(item => new EmployeeDocumentDto
                {
                    Id = item.Id,
                    DocumentName = item.DocumentName,
                    FileName = item.FileName,
                    UploadDate = item.UploadDate,
                    UploadedFileExtention = item.UploadedFileExtention,
                    UploadedFile = item.UploadedFile ?? Array.Empty<byte>(),
                    SetDate = item.SetDate,
                    CreatedBy = item.CreatedBy
                }).ToList(),
                EmployeeFamilyInfos = employee.EmployeeFamilyInfos.Select(item => new EmployeeFamilyInfoDto
                {
                    Id = item.Id,
                    IdGender = item.IdGender,
                    IdRelationship = item.IdRelationship,
                    Name = item.Name,
                    DateOfBirth = item.DateOfBirth,
                    ContactNo = item.ContactNo,
                    CurrentAddress = item.CurrentAddress,
                    PermanentAddress = item.PermanentAddress,
                    SetDate = item.SetDate,
                    CreatedBy = item.CreatedBy
                }).ToList(),
                EmployeeEducationInfos = employee.EmployeeEducationInfos.Select(item => new EmployeeEducationInfoDto
                {
                    Id = item.Id,
                    IdEducationLevel = item.IdEducationLevel,
                    IdEducationExamination = item.IdEducationExamination,
                    IdEducationResult = item.IdEducationResult,
                    Cgpa = item.Cgpa,
                    ExamScale = item.ExamScale,
                    Marks = item.Marks,
                    Major = item.Major,
                    PassingYear = item.PassingYear,
                    InstituteName = item.InstituteName,
                    IsForeignInstitute = item.IsForeignInstitute,
                    Duration = item.Duration,
                    Achievement = item.Achievement,
                    SetDate = item.SetDate,
                    CreatedBy = item.CreatedBy
                }).ToList(),
                EmployeeProfessionalCertifications = employee.EmployeeProfessionalCertifications.Select(item => new EmployeeProfessionalCertificationDto
                {
                    Id = item.Id,
                    CertificationTitle = item.CertificationTitle,
                    CertificationInstitute = item.CertificationInstitute,
                    InstituteLocation = item.InstituteLocation,
                    FromDate = item.FromDate,
                    ToDate = item.ToDate,
                    SetDate = item.SetDate,
                    CreatedBy = item.CreatedBy
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        return employee is null ? null : employee;
    }

    public async Task<int> CreateEmployeeAsync(EmployeeCreateDto dto, CancellationToken cancellationToken)
    {
        var employee = new Employee
        {
            IdClient = dto.IdClient,
            EmployeeName = dto.EmployeeName,
            EmployeeNameBangla = dto.EmployeeNameBangla,
            EmployeeImage = dto.EmployeeImage,
            FatherName = dto.FatherName,
            MotherName = dto.MotherName,
            IdReportingManager = dto.IdReportingManager,
            IdJobType = dto.IdJobType,
            IdEmployeeType = dto.IdEmployeeType,
            BirthDate = dto.BirthDate,
            JoiningDate = dto.JoiningDate,
            IdGender = dto.IdGender,
            IdReligion = dto.IdReligion,
            IdDepartment = dto.IdDepartment,
            IdSection = dto.IdSection,
            IdDesignation = dto.IdDesignation,
            HasOvertime = dto.HasOvertime,
            HasAttendenceBonus = dto.HasAttendenceBonus,
            IdWeekOff = dto.IdWeekOff,
            Address = dto.Address,
            PresentAddress = dto.PresentAddress,
            NationalIdentificationNumber = dto.NationalIdentificationNumber,
            ContactNo = dto.ContactNo,
            IdMaritalStatus = dto.IdMaritalStatus,
            IsActive = dto.IsActive,
            SetDate = dto.SetDate,
            CreatedBy = dto.CreatedBy
        };

        PopulateEmployeeDetails(dto, employee);

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }

    public async Task<int> UpdateEmployeeAsync(EmployeeUpdateDto dto, CancellationToken cancellationToken)
    {
        var existingEmployee = await _context.Employees
            .Include(item => item.EmployeeDocuments)
            .Include(item => item.EmployeeFamilyInfos)
            .Include(item => item.EmployeeEducationInfos)
            .Include(item => item.EmployeeProfessionalCertifications)
            .FirstOrDefaultAsync(item => item.Id == dto.Id, cancellationToken);

        if (existingEmployee is null)
        {
            throw new KeyNotFoundException("Employee not found.");
        }

        existingEmployee.EmployeeName = dto.EmployeeName;
        existingEmployee.EmployeeNameBangla = dto.EmployeeNameBangla;
        existingEmployee.EmployeeImage = dto.EmployeeImage;
        existingEmployee.FatherName = dto.FatherName;
        existingEmployee.MotherName = dto.MotherName;
        existingEmployee.IdReportingManager = dto.IdReportingManager;
        existingEmployee.IdJobType = dto.IdJobType;
        existingEmployee.IdEmployeeType = dto.IdEmployeeType;
        existingEmployee.BirthDate = dto.BirthDate;
        existingEmployee.JoiningDate = dto.JoiningDate;
        existingEmployee.IdGender = dto.IdGender;
        existingEmployee.IdReligion = dto.IdReligion;
        existingEmployee.IdDepartment = dto.IdDepartment;
        existingEmployee.IdSection = dto.IdSection;
        existingEmployee.IdDesignation = dto.IdDesignation;
        existingEmployee.HasOvertime = dto.HasOvertime;
        existingEmployee.HasAttendenceBonus = dto.HasAttendenceBonus;
        existingEmployee.IdWeekOff = dto.IdWeekOff;
        existingEmployee.Address = dto.Address;
        existingEmployee.PresentAddress = dto.PresentAddress;
        existingEmployee.NationalIdentificationNumber = dto.NationalIdentificationNumber;
        existingEmployee.ContactNo = dto.ContactNo;
        existingEmployee.IdMaritalStatus = dto.IdMaritalStatus;
        existingEmployee.IsActive = dto.IsActive;
        existingEmployee.SetDate = dto.SetDate;
        existingEmployee.CreatedBy = dto.CreatedBy;

        UpdateDocuments(existingEmployee, dto);
        UpdateFamilyInfos(existingEmployee, dto);
        UpdateEducationInfos(existingEmployee, dto);
        UpdateCertifications(existingEmployee, dto);

        await _context.SaveChangesAsync(cancellationToken);

        return existingEmployee.Id;
    }

    public async Task DeleteEmployeeAsync(int id, CancellationToken cancellationToken)
    {
        var existingEmployee = await _context.Employees.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);

        if (existingEmployee is null)
        {
            return;
        }

        existingEmployee.IsActive = false;

        await _context.SaveChangesAsync(cancellationToken);
    }

    private void PopulateEmployeeDetails(EmployeeCreateDto dto, Employee employee)
    {
        foreach (var item in dto.EmployeeDocuments)
        {
            employee.EmployeeDocuments.Add(new EmployeeDocument
            {
                IdClient = dto.IdClient,
                DocumentName = item.DocumentName,
                FileName = item.FileName,
                UploadDate = item.UploadDate,
                UploadedFileExtention = item.UploadedFileExtention,
                UploadedFile = item.UploadedFile,
                SetDate = item.SetDate,
                CreatedBy = item.CreatedBy
            });
        }

        foreach (var item in dto.EmployeeFamilyInfos)
        {
            employee.EmployeeFamilyInfos.Add(new EmployeeFamilyInfo
            {
                IdClient = dto.IdClient,
                Name = item.Name,
                IdGender = item.IdGender,
                IdRelationship = item.IdRelationship,
                DateOfBirth = item.DateOfBirth,
                ContactNo = item.ContactNo,
                CurrentAddress = item.CurrentAddress,
                PermanentAddress = item.PermanentAddress,
                SetDate = item.SetDate,
                CreatedBy = item.CreatedBy
            });
        }

        foreach (var item in dto.EmployeeEducationInfos)
        {
            employee.EmployeeEducationInfos.Add(new EmployeeEducationInfo
            {
                IdClient = dto.IdClient,
                IdEducationLevel = item.IdEducationLevel,
                IdEducationExamination = item.IdEducationExamination,
                IdEducationResult = item.IdEducationResult,
                Cgpa = item.Cgpa,
                ExamScale = item.ExamScale,
                Marks = item.Marks,
                Major = item.Major,
                PassingYear = item.PassingYear,
                InstituteName = item.InstituteName,
                IsForeignInstitute = item.IsForeignInstitute,
                Duration = item.Duration,
                Achievement = item.Achievement,
                SetDate = item.SetDate,
                CreatedBy = item.CreatedBy
            });
        }

        foreach (var item in dto.EmployeeProfessionalCertifications)
        {
            employee.EmployeeProfessionalCertifications.Add(new EmployeeProfessionalCertification
            {
                IdClient = dto.IdClient,
                CertificationTitle = item.CertificationTitle,
                CertificationInstitute = item.CertificationInstitute,
                InstituteLocation = item.InstituteLocation,
                FromDate = item.FromDate,
                ToDate = item.ToDate,
                SetDate = item.SetDate,
                CreatedBy = item.CreatedBy
            });
        }
    }

    private void UpdateDocuments(Employee employee, EmployeeUpdateDto dto)
    {
        var existing = employee.EmployeeDocuments;

        var toDelete = existing
            .Where(x => !dto.EmployeeDocuments.Any(d => d.Id == x.Id))
            .ToList();

        _context.EmployeeDocuments.RemoveRange(toDelete);

        foreach (var item in dto.EmployeeDocuments)
        {
            var entity = existing.FirstOrDefault(x => x.Id == item.Id);

            if (entity != null)
            {
                entity.DocumentName = item.DocumentName;
                entity.FileName = item.FileName;
                entity.UploadDate = item.UploadDate;
                entity.UploadedFileExtention = item.UploadedFileExtention;
                entity.UploadedFile = item.UploadedFile;
                entity.SetDate = item.SetDate;
                entity.CreatedBy = item.CreatedBy;
            }
            else
            {
                employee.EmployeeDocuments.Add(new EmployeeDocument
                {
                    IdClient = dto.IdClient,
                    DocumentName = item.DocumentName,
                    FileName = item.FileName,
                    UploadDate = item.UploadDate,
                    UploadedFileExtention = item.UploadedFileExtention,
                    UploadedFile = item.UploadedFile,
                    SetDate = item.SetDate,
                    CreatedBy = item.CreatedBy
                });
            }
        }
    }

    private void UpdateFamilyInfos(Employee employee, EmployeeUpdateDto dto)
    {
        var existing = employee.EmployeeFamilyInfos;

        var toDelete = existing
            .Where(x => !dto.EmployeeFamilyInfos.Any(d => d.Id == x.Id))
            .ToList();

        _context.EmployeeFamilyInfos.RemoveRange(toDelete);

        foreach (var item in dto.EmployeeFamilyInfos)
        {
            var entity = existing.FirstOrDefault(x => x.Id == item.Id);

            if (entity != null)
            {
                entity.Name = item.Name;
                entity.IdGender = item.IdGender;
                entity.IdRelationship = item.IdRelationship;
                entity.DateOfBirth = item.DateOfBirth;
                entity.ContactNo = item.ContactNo;
                entity.CurrentAddress = item.CurrentAddress;
                entity.PermanentAddress = item.PermanentAddress;
                entity.SetDate = item.SetDate;
                entity.CreatedBy = item.CreatedBy;
            }
            else
            {
                employee.EmployeeFamilyInfos.Add(new EmployeeFamilyInfo
                {
                    IdClient = dto.IdClient,
                    Name = item.Name,
                    IdGender = item.IdGender,
                    IdRelationship = item.IdRelationship,
                    DateOfBirth = item.DateOfBirth,
                    ContactNo = item.ContactNo,
                    CurrentAddress = item.CurrentAddress,
                    PermanentAddress = item.PermanentAddress,
                    SetDate = item.SetDate,
                    CreatedBy = item.CreatedBy
                });
            }
        }
    }

    private void UpdateEducationInfos(Employee employee, EmployeeUpdateDto dto)
    {
        var existing = employee.EmployeeEducationInfos;

        var toDelete = existing
            .Where(x => !dto.EmployeeEducationInfos.Any(d => d.Id == x.Id))
            .ToList();

        _context.EmployeeEducationInfos.RemoveRange(toDelete);

        foreach (var item in dto.EmployeeEducationInfos)
        {
            var entity = existing.FirstOrDefault(x => x.Id == item.Id);

            if (entity != null)
            {
                entity.IdEducationLevel = item.IdEducationLevel;
                entity.IdEducationExamination = item.IdEducationExamination;
                entity.IdEducationResult = item.IdEducationResult;
                entity.Cgpa = item.Cgpa;
                entity.ExamScale = item.ExamScale;
                entity.Marks = item.Marks;
                entity.Major = item.Major;
                entity.PassingYear = item.PassingYear;
                entity.InstituteName = item.InstituteName;
                entity.IsForeignInstitute = item.IsForeignInstitute;
                entity.Duration = item.Duration;
                entity.Achievement = item.Achievement;
                entity.SetDate = item.SetDate;
                entity.CreatedBy = item.CreatedBy;
            }
            else
            {
                employee.EmployeeEducationInfos.Add(new EmployeeEducationInfo
                {
                    IdClient = dto.IdClient,
                    IdEducationLevel = item.IdEducationLevel,
                    IdEducationExamination = item.IdEducationExamination,
                    IdEducationResult = item.IdEducationResult,
                    Cgpa = item.Cgpa,
                    ExamScale = item.ExamScale,
                    Marks = item.Marks,
                    Major = item.Major,
                    PassingYear = item.PassingYear,
                    InstituteName = item.InstituteName,
                    IsForeignInstitute = item.IsForeignInstitute,
                    Duration = item.Duration,
                    Achievement = item.Achievement,
                    SetDate = item.SetDate,
                    CreatedBy = item.CreatedBy
                });
            }
        }
    }

    private void UpdateCertifications(Employee employee, EmployeeUpdateDto dto)
    {
        var existing = employee.EmployeeProfessionalCertifications;

        var toDelete = existing
            .Where(x => !dto.EmployeeProfessionalCertifications.Any(d => d.Id == x.Id))
            .ToList();

        _context.EmployeeProfessionalCertifications.RemoveRange(toDelete);

        foreach (var item in dto.EmployeeProfessionalCertifications)
        {
            var entity = existing.FirstOrDefault(x => x.Id == item.Id);

            if (entity != null)
            {
                entity.CertificationTitle = item.CertificationTitle;
                entity.CertificationInstitute = item.CertificationInstitute;
                entity.InstituteLocation = item.InstituteLocation;
                entity.FromDate = item.FromDate;
                entity.ToDate = item.ToDate;
                entity.SetDate = item.SetDate;
                entity.CreatedBy = item.CreatedBy;
            }
            else
            {
                employee.EmployeeProfessionalCertifications.Add(new EmployeeProfessionalCertification
                {
                    IdClient = dto.IdClient,
                    CertificationTitle = item.CertificationTitle,
                    CertificationInstitute = item.CertificationInstitute,
                    InstituteLocation = item.InstituteLocation,
                    FromDate = item.FromDate,
                    ToDate = item.ToDate,
                    SetDate = item.SetDate,
                    CreatedBy = item.CreatedBy
                });
            }
        }
    }
}
