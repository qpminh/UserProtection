using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Application.Interfaces.Cores;

namespace UserProtection.Application.Services.Cores
{
    public class AppAdminConfigService : IAppAdminConfigService
    {
        public string AdminEmail { get; }
        public string AdminUsername { get; }

        public AppAdminConfigService(IConfiguration config)
        {
            AdminEmail = config["DefaultAdmin:Email"];
            AdminUsername = config["DefaultAdmin:Username"];
        }
    }
}
