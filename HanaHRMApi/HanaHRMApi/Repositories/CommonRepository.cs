using HanaHRMApi.DTOs.Common;
using HanaHRMApi.Models;
using HanaHRMApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HanaHRMApi.Repositories;

public class CommonRepository(HanaHrmContext context) : ICommonRepository
{
    private readonly HanaHrmContext _context = context;


    public Task<List<DropdownItemDto>> GetDepartmentsAsync(CancellationToken cancellationToken)
    {
        return _context.Departments
            .AsNoTracking()
            .OrderBy(item => item.DepartName)
            .Select(item => new DropdownItemDto
            {
                Id = item.Id,
                Name = item.DepartName
            })
            .ToListAsync(cancellationToken);
    }

    public Task<List<DropdownItemDto>> GetDesignationsAsync(CancellationToken cancellationToken)
    {
        return _context.Designations
            .AsNoTracking()
            .OrderBy(item => item.DesignationName)
            .Select(item => new DropdownItemDto
            {
                Id = item.Id,
                Name = item.DesignationName
            })
            .ToListAsync(cancellationToken);
    }

    public Task<List<DropdownItemDto>> GetGendersAsync(CancellationToken cancellationToken)
    {
        return _context.Genders
            .AsNoTracking()
            .OrderBy(item => item.GenderName)
            .Select(item => new DropdownItemDto
            {
                Id = item.Id,
                Name = item.GenderName ?? string.Empty,
            })
            .ToListAsync(cancellationToken);
    }

    public Task<List<DropdownItemDto>> GetJobTypesAsync(CancellationToken cancellationToken)
    {
        return _context.JobTypes
            .AsNoTracking()
            .OrderBy(item => item.JobTypeName)
            .Select(item => new DropdownItemDto
            {
                Id = item.Id,
                Name = item.JobTypeName
            })
            .ToListAsync(cancellationToken);
    }

    public Task<List<DropdownItemDto>> GetEmployeeTypesAsync(CancellationToken cancellationToken)
    {
        return _context.EmployeeTypes
            .AsNoTracking()
            .OrderBy(item => item.TypeName)
            .Select(item => new DropdownItemDto
            {
                Id = item.Id,
                Name = item.TypeName ?? string.Empty
            })
            .ToListAsync(cancellationToken);
    }

    public Task<List<DropdownItemDto>> GetMaritalStatusesAsync(CancellationToken cancellationToken)
    {
        return _context.MaritalStatuses
            .AsNoTracking()
            .OrderBy(item => item.MaritalStatusName)
            .Select(item => new DropdownItemDto
            {
                Id = item.Id,
                Name = item.MaritalStatusName
            })
            .ToListAsync(cancellationToken);
    }

    public Task<List<DropdownItemDto>> GetReligionsAsync(CancellationToken cancellationToken)
    {
        return _context.Religions
            .AsNoTracking()
            .OrderBy(item => item.ReligionName)
            .Select(item => new DropdownItemDto
            {
                Id = item.Id,
                Name = item.ReligionName
            })
            .ToListAsync(cancellationToken);
    }

    public Task<List<DropdownItemDto>> GetSectionsAsync(CancellationToken cancellationToken)
    {
        return _context.Sections
            .AsNoTracking()
            .OrderBy(item => item.SectionName)
            .Select(item => new DropdownItemDto
            {
                Id = item.Id,
                Name = item.SectionName
            })
            .ToListAsync(cancellationToken);
    }

    public Task<List<DropdownItemDto>> GetWeekOffsAsync(CancellationToken cancellationToken)
    {
        return _context.WeekOffs
            .AsNoTracking()
            .OrderBy(item => item.WeekOffDay)
            .Select(item => new DropdownItemDto
            {
                Id = item.Id,
                Name = item.WeekOffDay ?? string.Empty
            })
            .ToListAsync(cancellationToken);
    }

    public Task<List<DropdownItemDto>> GetRelationshipsAsync(CancellationToken cancellationToken)
    {
        return _context.Relationships
            .AsNoTracking()
            .OrderBy(item => item.RelationName)
            .Select(item => new DropdownItemDto
            {
                Id = item.Id,
                Name = item.RelationName
            })
            .ToListAsync(cancellationToken);
    }

    public Task<List<DropdownItemDto>> GetEducationLevelsAsync(CancellationToken cancellationToken)
    {
        return _context.EducationLevels
            .AsNoTracking()
            .OrderBy(item => item.EducationLevelName)
            .Select(item => new DropdownItemDto
            {
                Id = item.Id,
                Name = item.EducationLevelName
            })
            .ToListAsync(cancellationToken);
    }

    public Task<List<DropdownItemDto>> GetEducationExaminationsAsync(CancellationToken cancellationToken)
    {
        return _context.EducationExaminations
            .AsNoTracking()
            .OrderBy(item => item.ExamName)
            .Select(item => new DropdownItemDto
            {
                Id = item.Id,
                Name = item.ExamName
            })
            .ToListAsync(cancellationToken);
    }

    public Task<List<DropdownItemDto>> GetEducationResultsAsync(CancellationToken cancellationToken)
    {
        return _context.EducationResults
            .AsNoTracking()
            .OrderBy(item => item.ResultName)
            .Select(item => new DropdownItemDto
            {
                Id = item.Id,
                Name = item.ResultName
            })
            .ToListAsync(cancellationToken);
    }
}
