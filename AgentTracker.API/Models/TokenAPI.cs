using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentTracker.API.Models
{ 
    public class TokenInfoAPI
    {
        public string at_hash { get; set; }
        public string sub { get; set; }
        public bool email_verified { get; set; }
        public string iss { get; set; }

        [JsonProperty("cognito:username")]
        public string CognitoUsername { get; set; }
        public string given_name { get; set; }
        public string aud { get; set; }
        public string token_use { get; set; }
        public int auth_time { get; set; }
        public int exp { get; set; }
        public int iat { get; set; }
        public string family_name { get; set; }
        public string email { get; set; }
        public string inclusion_tags { get; set; }
        public string agent_id { get; set; }
        public string agency { get; set; }
        public bool is_agency_admin { get; set; }
        public bool is_admin { get; set; } 
    } 
}
