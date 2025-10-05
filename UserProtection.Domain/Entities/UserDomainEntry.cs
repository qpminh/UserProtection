namespace UserProtection.Domain.Entities;

public partial class UserDomainEntry
{
    public long EntryId { get; set; }

    public string? UserId { get; set; }

    public string Domain { get; set; } = null!;

    public bool? Safe { get; set; }

    public int? EntryType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Status { get; set; }

    public virtual User? User { get; set; }
}


