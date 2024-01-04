using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentTracker.API.Models
{
    public class AgentDetailsViewModel
    {
        public List<MemberList> memberLists { get; set; }
        public int TotalCount { get; set; }
    }
    public class MemberList
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Hicn { get; set; }
        public string MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PlanId { get; set; }
        public long AgentId { get; set; }
        public string AgentName { get; set; }
        public DateTime ? ReceiptDate { get; set; }
        public DateTime ? EffectiveDate { get; set; }
        public string ApplicationStatus { get; set; }
        public string AppNbr { get; set; }
        public string PcpId { get; set; }
        public string PcpName { get; set; }
        public int PBPID { get; set; }
        public string State { get; set; } 
    }
}
