using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentTracker.API.Models
{
    public class UserListResponse
    {
        public List<UserListDetail> userListDetails { get; set; }
    }
    public class UserListDetail
    {
        public int id { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string agent_id { get; set; }
        public string whitelist_status { get; set; }
        public int? agency { get; set; }
        public string agency_name { get; set; }
        public bool is_agency_admin { get; set; }
        public bool is_superuser { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string inclusion_tags { get; set; }
    }
}
