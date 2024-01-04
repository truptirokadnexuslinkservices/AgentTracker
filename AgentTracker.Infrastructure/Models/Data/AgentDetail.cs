using System;
using System.Collections.Generic;

#nullable disable

namespace AgentTracker.Infrastructure.Models.data
{
    public partial class AgentDetail
    {
        public int Id { get; set; }
        public int? ApplicationId { get; set; }
        public string Hicn { get; set; }
        public string MbiHic { get; set; }
        public string MemberId { get; set; }
        public string FirstName { get; set; }
        public string MiddleInit { get; set; }
        public string LastName { get; set; }
        public string PlanId { get; set; }
        public int? PbpId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? Zip { get; set; }
        public string County { get; set; }
        public long? AgentId { get; set; }
        public string AgentName { get; set; }
        public string ApplicationCategory { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public DateTime? SgnDate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string ApplicationStatus { get; set; }
        public string AppNbr { get; set; }
        public string PcpId { get; set; }
        public DateTime? PcpEffStartDate { get; set; }
        public DateTime? PcpEffEndDateDate { get; set; }
        public string PcpName { get; set; }
        public string ElectionType { get; set; }
        public string SepReasonCode { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
