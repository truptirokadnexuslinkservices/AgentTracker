using AgentTracker.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentTracker.API.Interfaces
{
    public interface IAgent
    {
        Task<ResponseModel<AgentDetailsViewModel>> GetAgentList(string SearchString, int AppNo, string MedicareID, string MemberID,
          string FirstName, string LastName, DateTime? ReceiptDate, DateTime? EffectiveDate, string Status, double AgentID, string PCPName,
        string Agent, int SkipCount, int TakeCount, string State, string PlanID, int PBPID,string Email,bool IsSuperuser,double LoggedAgentId);
        Task<ResponseModel<TokenInfoAPI>> GetTokenInfo(string Code, string redirect_uri);
        //  Task<ResponseModel<TokenResponse>> GetTokenInfo(string Code, string redirect_uri); 
        Task<ResponseModel<TokenDetails>> GetToken(string Code, string redirect_uri);
        Task<ResponseModel<MemberCardModel>> GetMemberCardDetailsById(string MemberId);
        //Task<ResponseModel<UserListResponse>> GetStagingUsersList(string token);
        Task<ResponseModel<UserListResponse>> GetStagingUsersList();
        //Task<ResponseModel<UserListResponse>> GetProductionUsersList(string token);
    }
}
