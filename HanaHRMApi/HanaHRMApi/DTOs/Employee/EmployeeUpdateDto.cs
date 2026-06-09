using System.ComponentModel.DataAnnotations;

namespace HanaHRMApi.DTOs.Employee
{
    public class EmployeeUpdateDto : EmployeeBaseDto
    {
        [Required]
        public int Id { get; set; }
    }
}
