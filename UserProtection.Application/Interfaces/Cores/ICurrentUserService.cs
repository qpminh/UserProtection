namespace UserProtection.Application.Interfaces.Cores
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? UserName { get; }
        string? Role { get; }
        int? AssociatedId { get; }
    }
}
