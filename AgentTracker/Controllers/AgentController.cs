using AgentTracker.API.Interfaces;
using AgentTracker.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AgentTracker.Controllers
{
    // [Route("pdfeditor")]
    [AllowAnonymous]
    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgent _agent;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AgentController> _logger;
        public AgentController(IAgent agent, IConfiguration configuration, ILogger<AgentController> logger)
        {
            _configuration = configuration;
            _agent = agent;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet("SSOSignIn")]
        public string Login([FromQuery] string redirect_uri)
        {
            //var redirectURL = "https://auth.prominencemabroker.com/login?" +
            var redirectURL = _configuration["SSOUrl"].ToString() + 
                             "&client_id=" + _configuration["AWSCognitoIntegration:client_id"].ToString() +
                             "&response_type=code" +
                             "&scope=" + _configuration["AWSCognitoIntegration:scope"].ToString() +
                             "&redirect_uri=" + redirect_uri; // _configuration["AWSCognitoIntegration:redirect_uri"].ToString();

            //var redirectURL = "https://prominence-auth-stg.auth.us-west-2.amazoncognito.com/login?" +
            //                  "client_id=" + _configuration["AWSCognitoIntegration:client_id"].ToString() +
            //                  "&response_type=code" +
            //                  "&scope=" + _configuration["AWSCognitoIntegration:scope"].ToString() +
            //                  "&redirect_uri=" + redirect_uri;
            return redirectURL;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GetAccessToken")]
        public Task<ResponseModel<TokenDetails>> GetToken([FromQuery] string Code, string redirect_uri)
        {
            try
            {
                Task<ResponseModel<TokenDetails>> tokenAPI;
                tokenAPI = _agent.GetToken(Code, redirect_uri);
                return tokenAPI;
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(500), ex, "Error while processing request {0}", ex);
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RetrievingDataFromCognito")]
        public Task<ResponseModel<TokenInfoAPI>> GetLoggedUserInfo([FromQuery] string Code, string redirect_uri)
        {
            try
            {
                Task<ResponseModel<TokenInfoAPI>> tokenAPI;
                tokenAPI = _agent.GetTokenInfo(Code, redirect_uri);
                return tokenAPI;
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(500), ex, "Error while processing request {0}", ex);
                throw;
            }
        }


        //public Task<ResponseModel<TokenResponse>> GetLoggedUserInfo([FromQuery] string Code, string redirect_uri)
        //{
        //    try
        //    {
        //        //  Task<ResponseModel<TokenInfoAPI>> tokenAPI;
        //        Task<ResponseModel<TokenResponse>> tokenAPI;
        //        tokenAPI = _agent.GetTokenInfo(Code, redirect_uri);
        //        return tokenAPI;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(new EventId(500), ex, "Error while processing request {0}", ex);
        //        throw;
        //    }
        //}

        //[AllowAnonymous]
        [HttpGet]
        [Route("MemberList")]
        public Task<ResponseModel<AgentDetailsViewModel>> GetMemberList([FromQuery] string SearchString, int AppNo, string MedicareID, string MemberID,
          string FirstName, string LastName, DateTime? ReceiptDate, DateTime? EffectiveDate, string Status, double AgentID, string PCPName,
        string Agent, int SkipCount, int TakeCount, string State, string PlanID, int PBPID, string Email,bool IsSuperuser,double LoggedAgentId)
        {
            try
            {
                Task<ResponseModel<AgentDetailsViewModel>> agentDetails;
                agentDetails = _agent.GetAgentList(SearchString, AppNo, MedicareID, MemberID, FirstName, LastName, ReceiptDate,
                                                    EffectiveDate, Status, AgentID, PCPName, Agent, SkipCount, TakeCount, State,
                                                    PlanID, PBPID, Email, IsSuperuser, LoggedAgentId);
                return agentDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(500), ex, "Error while processing request {0}", ex);
                throw;
            }
        }

        [HttpGet]
        [Route("GetMemberCardDetailsById")]
        public Task<ResponseModel<MemberCardModel>> GetMemberCardDetailsById([FromQuery] string MemberId)
        {
            try
            {
                Task<ResponseModel<MemberCardModel>> memberCardModel;
                memberCardModel = _agent.GetMemberCardDetailsById(MemberId);
                return memberCardModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(500), ex, "Error while processing request {0}", ex);
                throw;
            }
        }

        [HttpGet]
        [Route("GetStagingUsersList")]
        //public Task<ResponseModel<UserListResponse>> GetStagingUsersList([FromQuery] string token)
        public Task<ResponseModel<UserListResponse>> GetStagingUsersList()
        {
            try
            {
                // System.IO.File.AppendAllTextAsync(Environment.CurrentDirectory + "\\testtxt.txt", DateTime.Now + " : Token :- " + token + "\n");
                Task<ResponseModel<UserListResponse>> stagingUsersList;
                // stagingUsersList = _agent.GetStagingUsersList(token);
                stagingUsersList = _agent.GetStagingUsersList();
                return stagingUsersList;
            }
            catch (Exception ex)
            {
                // System.IO.File.AppendAllTextAsync(Environment.CurrentDirectory + "\\testtxt.txt", DateTime.Now + " : " + ex.Message + "\n");
                _logger.LogError(new EventId(500), ex, "Error while processing request {0}", ex);
                throw;
            }
        }

        //[HttpGet]
        //[Route("GetProductionUsersList")]
        //public Task<ResponseModel<UserListResponse>> GetProductionUsersList([FromQuery] string token)
        //{
        //    try
        //    {
        //        Task<ResponseModel<UserListResponse>> productionUsersList;
        //        productionUsersList = _agent.GetProductionUsersList(token);
        //        return productionUsersList;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(new EventId(500), ex, "Error while processing request {0}", ex);
        //        throw;
        //    }
        //}

    }
}
