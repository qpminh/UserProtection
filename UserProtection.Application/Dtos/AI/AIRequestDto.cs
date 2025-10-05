using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserProtection.Application.Dtos.AI
{
    public class AIRequest
    {
        public string Prompt { get; set; }
    }
}
