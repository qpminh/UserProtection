using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProtection.Application.Interfaces.Cores
{
    public interface IAppAdminConfigService
    {
        string AdminEmail { get; }
        string AdminUsername { get; }
    }
}
