namespace UserProtection.Application.Dtos.Cores
{
    public class UserDto
    {
        public string Id { get; set; } = null!;

        public int? TenantId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Status { get; set; } = null!;
    }

    public class RegisterDto
    {
        public int? TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
