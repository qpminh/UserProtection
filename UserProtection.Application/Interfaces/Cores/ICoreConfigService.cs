namespace UserProtection.Application.Interfaces.Cores
{
    public interface IAppAdminConfigService
    {
        string AdminEmail { get; }
        string AdminUsername { get; }
    }
}
