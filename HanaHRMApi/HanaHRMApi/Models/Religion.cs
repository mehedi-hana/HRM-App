namespace HanaHRMApi.Models;

public class Religion
{
    public int IdClient { get; set; }

    public int Id { get; set; }

    public string ReligionName { get; set; } = null!;

    public DateTime? SetDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
