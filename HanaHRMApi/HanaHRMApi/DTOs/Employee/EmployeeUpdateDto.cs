using System.ComponentModel.DataAnnotations;

namespace HanaHRMApi.DTOs.Employee
{
    public class EmployeeUpdateDto : EmployeeBaseDto
    {
        public int Id { get; set; }
    }
}
