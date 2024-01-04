using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentTracker.API.Models
{
    public class TokenResponse
    {
        public TokenDetails tokenDetails { get; set; }
        public TokenInfoAPI tokenInfo { get; set; }
    }
}
