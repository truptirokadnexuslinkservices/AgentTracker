using AgentTracker.API.Interfaces;
using AgentTracker.API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static AgentTracker.API.Models.Responses;


namespace AgentTracker.Infrastructure.Repository
{
    public class AgentRepository : IAgent
    {
        private readonly ILogger<AgentRepository> _logger;
        private readonly IConfiguration _config;

        public AgentRepository(IConfiguration config, ILogger<AgentRepository> logger)
        {
            _config = config;
            _logger = logger;
        }
        public async Task<ResponseModel<AgentDetailsViewModel>> GetAgentList(string SearchString, int AppNo, string MedicareID, string MemberID,
           string FirstName, string LastName, DateTime? ReceiptDate, DateTime? EffectiveDate, string Status, double AgentID, string PCPName,
         string Agent, int SkipCount, int TakeCount, string State, string PlanID, int PBPID, string Email, bool IsSuperuser, double LoggedAgentId)
        {
            ResponseModel<AgentDetailsViewModel> response = new();
            try
            {
                AgentDetailsViewModel agentDetailsViewModel = new AgentDetailsViewModel();
                List<MemberList> memberLists = new List<MemberList>();
                DataTable dtAgent = new DataTable();
                string conn = _config.GetConnectionString("DefaultConnection").ToString();
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "usp_GetAgentList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SearchString", SearchString);
                    cmd.Parameters.AddWithValue("@AppNo", AppNo);
                    cmd.Parameters.AddWithValue("@MedicareID", MedicareID);
                    cmd.Parameters.AddWithValue("@MemberID", MemberID);
                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@ReceiptDate", ReceiptDate != null ? ReceiptDate : "");
                    cmd.Parameters.AddWithValue("@EffectiveDate", EffectiveDate != null ? EffectiveDate : "");
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.Parameters.AddWithValue("@AgentID", AgentID);
                    cmd.Parameters.AddWithValue("@PCPName", PCPName);
                    cmd.Parameters.AddWithValue("@Agent", Agent);
                    cmd.Parameters.AddWithValue("@SkipCount", SkipCount);
                    cmd.Parameters.AddWithValue("@TakeCount", TakeCount);
                    cmd.Parameters.AddWithValue("@State", State);
                    cmd.Parameters.AddWithValue("@PlanID", PlanID);
                    cmd.Parameters.AddWithValue("@PBPID", PBPID);
                    cmd.Parameters.AddWithValue("@email", Email.Replace(' ', '+'));
                    cmd.Parameters.AddWithValue("@is_superuser", IsSuperuser);
                    cmd.Parameters.AddWithValue("@LoggedAgentId", LoggedAgentId);

                    //  cmd.Parameters.AddWithValue("@TotalCount", ParameterDirection.Output);
                    SqlParameter parmOUT = new SqlParameter("@TotalCount", SqlDbType.Int);
                    parmOUT.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parmOUT);

                    cmd.ExecuteNonQuery();

                    agentDetailsViewModel.TotalCount = (int)cmd.Parameters["@TotalCount"].Value;

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtAgent);
                    //string retunvalue = (string)cmd.Parameters["@TotalCount"].Value;
                    for (int i = 0; i < dtAgent.Rows.Count; i++)
                    {

                        var list = new MemberList();
                        if (dtAgent.Rows[i]["Receipt Date"].ToString() != "")
                        {
                            list.ReceiptDate = Convert.ToDateTime(dtAgent.Rows[i]["Receipt Date"]);
                        }
                        
                        if (dtAgent.Rows[i]["Effective Date"].ToString() != "")
                        {
                            list.EffectiveDate = Convert.ToDateTime(dtAgent.Rows[i]["Effective Date"]);
                        }
                        list.Id = (int)dtAgent.Rows[i]["ID"];
                        list.ApplicationId = (int)dtAgent.Rows[i]["Application ID"];
                        list.Hicn = dtAgent.Rows[i]["HICN"].ToString();
                        list.MemberId = dtAgent.Rows[i]["Member ID"].ToString();
                        list.FirstName = dtAgent.Rows[i]["First Name"].ToString();
                        list.LastName = dtAgent.Rows[i]["Last Name"].ToString();
                        //list.ReceiptDate = Convert.ToDateTime(dtAgent.Rows[i]["Receipt Date"]),
                        //list.EffectiveDate = Convert.ToDateTime(dtAgent.Rows[i]["Effective Date"]),
                        list.ApplicationStatus = dtAgent.Rows[i]["Application Status"].ToString();
                        list.AgentId = (long)dtAgent.Rows[i]["Agent ID"];
                        list.AgentName = dtAgent.Rows[i]["Agent Name"].ToString();
                        list.PcpName = dtAgent.Rows[i]["PCP Name"].ToString();
                        list.PBPID = (int)dtAgent.Rows[i]["PBP ID"];
                        list.State = dtAgent.Rows[i]["State"].ToString();
                        list.PlanId = dtAgent.Rows[i]["Plan ID"].ToString();
                        //memberLists.Add(new MemberList
                        //{
                        //    Id = (int)dtAgent.Rows[i]["ID"],
                        //    ApplicationId = (int)dtAgent.Rows[i]["Application ID"],
                        //    Hicn = dtAgent.Rows[i]["HICN"].ToString(),
                        //    MemberId = dtAgent.Rows[i]["Member ID"].ToString(),
                        //    FirstName = dtAgent.Rows[i]["First Name"].ToString(),
                        //    LastName = dtAgent.Rows[i]["Last Name"].ToString(),
                        //    ReceiptDate = Convert.ToDateTime(dtAgent.Rows[i]["Receipt Date"]),
                        //    EffectiveDate = Convert.ToDateTime(dtAgent.Rows[i]["Effective Date"]),
                        //    ApplicationStatus = dtAgent.Rows[i]["Application Status"].ToString(),
                        //    AgentId = (long)dtAgent.Rows[i]["Agent ID"],
                        //    AgentName = dtAgent.Rows[i]["Agent Name"].ToString(),
                        //    PcpName = dtAgent.Rows[i]["PCP Name"].ToString(),
                        //    PBPID = (int)dtAgent.Rows[i]["PBP ID"],
                        //    State = dtAgent.Rows[i]["State"].ToString(),
                        //    PlanId = dtAgent.Rows[i]["Plan ID"].ToString(),
                        //});
                        memberLists.Add(list);
                    }
                    //agentDetailsViewModel.TotalCount = dtAgent.Rows.Count;
                    agentDetailsViewModel.memberLists = memberLists;

                    //if (dtAgent.Rows.Count > 0)
                    //{
                    response.ResponseObject = agentDetailsViewModel;
                    response.ResponseMessage = ResponseMessages[200];
                    response.ResponseCode = ResponseCode.Success;
                    //}
                    //else
                    //{
                    //    response.ResponseObject = null;
                    //    response.ResponseMessage = ResponseMessages[404];
                    //    response.ResponseCode =  ResponseCode.NotFound;
                    //}
                }

                return response; // agentDetailsViewModel;
            }
            catch (Exception ex)
            {
                response.ResponseObject = null;
                response.ResponseMessage = ResponseMessages[500];
                response.ResponseCode = ResponseCode.InterrnalError;
                _logger.LogError(new EventId(500), ex, "Error while processing request {0}", ex);
                throw;
            }
        }

        public async Task<ResponseModel<MemberCardModel>> GetMemberCardDetailsById(string MemberId)
        {
            ResponseModel<MemberCardModel> response = new();
            try
            {
                var tokenKey = _config["RedAPIToken"].ToString();

                MemberCardModel memberCardModel = new();
                string url = Constants.RedCardAPIURL;
                url = String.Format(url, tokenKey, MemberId);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                using HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                if (webResponse.Headers["pdfUrl"] != null)
                {
                    memberCardModel.TemproryUrl = webResponse.Headers["pdfUrl"];
                    memberCardModel.FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(memberCardModel.FilePath))
                    {
                        Directory.CreateDirectory(memberCardModel.FilePath);
                    }
                    _logger.LogError(new EventId(500), "Member Card TemproryUrl:- " + memberCardModel.TemproryUrl, "Member Card FilePath:- " + memberCardModel.FilePath + " MemberId :-" + MemberId);
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(memberCardModel.TemproryUrl, memberCardModel.FilePath + "/Card_" + MemberId + ".pdf");
                    }
                    response.ResponseObject = memberCardModel;
                    response.ResponseMessage = ResponseMessages[200];
                    response.ResponseCode = ResponseCode.Success;
                    _logger.LogError(new EventId(500), "Member Card Model:- " + memberCardModel);

                }
                else
                {
                    response.ResponseObject = null;
                    response.ResponseMessage = ResponseMessages[404];
                    response.ResponseCode = ResponseCode.NotFound;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseObject = null;
                response.ResponseMessage = ResponseMessages[500];
                response.ResponseCode = ResponseCode.InterrnalError;
                _logger.LogError(new EventId(500), ex, "Error while processing request {0}", ex);
                throw;
            }

        }

        public async Task<ResponseModel<TokenDetails>> GetToken(string Code, string redirect_uri)
        {
            ResponseModel<TokenDetails> response = new();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    _logger.LogError(new EventId(500), "Access Token Code :- " + Code);
                    //  using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://backend-stg.prominencemabroker.com/api/v1/oauth2/token/"))
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), _config["TokenAPI"]))
                    {
                        request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                        request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                            {
                                { "redirect_uri", redirect_uri },//_config["AWSCognitoIntegration:redirect_uri"]},
                                { "code",Code}
                            });
                        _logger.LogError(new EventId(500), "Access Token Request :- " + request.ToString());
                        var APIresponse = await httpClient.SendAsync(request);

                        var resultContent = await APIresponse.Content.ReadAsStringAsync();
                        _logger.LogError(new EventId(500), "Access Token Response :- " + resultContent);
                        if (APIresponse.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var key = JsonConvert.DeserializeObject<TokenDetails>(resultContent);
                            response.ResponseObject = key;
                            response.ResponseMessage = ResponseMessages[200];
                            response.ResponseCode = ResponseCode.Success;
                            return response;
                        }
                        else
                        {
                            response.ResponseObject = null;
                            response.ResponseMessage = ResponseMessages[404];
                            response.ResponseCode = ResponseCode.NotFound;
                            return response;
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                response.ResponseObject = null;
                response.ResponseMessage = ResponseMessages[500];
                response.ResponseCode = ResponseCode.InterrnalError;
                _logger.LogError(new EventId(500), ex, "Error while processing request {0}", ex);
                throw;
            }
        }

        public async Task<ResponseModel<TokenInfoAPI>> GetTokenInfo(string Code, string redirect_uri)
        {
            ResponseModel<TokenInfoAPI> response = new();
            try
            {
                using (var httpClient1 = new HttpClient())
                {
                    _logger.LogError(new EventId(500), "Token Info Code :- " + Code);
                    using (var request1 = new HttpRequestMessage(new HttpMethod("POST"), _config["TokenInfoAPI"]))
                    //  using (var request1 = new HttpRequestMessage(new HttpMethod("POST"), "https://backend-stg.prominencemabroker.com/api/v1/oauth2/token_info/"))
                    {
                        request1.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                        request1.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                            {
                                { "redirect_uri", redirect_uri },//_config["AWSCognitoIntegration:redirect_uri"]},
                                { "code",Code}
                            });
                        _logger.LogError(new EventId(500), "Token Info Request :- " + request1.ToString());
                        var APIresponse1 = await httpClient1.SendAsync(request1);

                        var resultContent1 = await APIresponse1.Content.ReadAsStringAsync();
                        _logger.LogError(new EventId(500), "Token Info Response :- " + resultContent1);
                        if (APIresponse1.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var key = JsonConvert.DeserializeObject<TokenInfoAPI>(resultContent1);
                            response.ResponseObject = key;
                            response.ResponseMessage = ResponseMessages[200];
                            response.ResponseCode = ResponseCode.Success;
                            return response;
                        }
                        else
                        {
                            response.ResponseObject = null;
                            response.ResponseMessage = ResponseMessages[404];
                            response.ResponseCode = ResponseCode.NotFound;
                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.ResponseObject = null;
                response.ResponseMessage = ResponseMessages[500];
                response.ResponseCode = ResponseCode.InterrnalError;
                _logger.LogError(new EventId(500), ex, "Error while processing request {0}", ex);
                throw;
            }
        }



        //public async Task<ResponseModel<TokenResponse>> GetTokenInfo(string Code, string redirect_uri)
        //{
        //    // ResponseModel<TokenDetails> response = new();
        //    TokenDetails tokenDetails = new TokenDetails();
        //    TokenInfoAPI tokenInfoAPI = new TokenInfoAPI();
        //    TokenResponse tokenResponse = new TokenResponse();
        //    ResponseModel<TokenResponse> response = new();
        //    try
        //    {
        //        using (var httpClient = new HttpClient())
        //        {
        //            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://backend-stg.prominencemabroker.com/api/v1/oauth2/token/"))
        //            {
        //                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
        //                request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        //                    {
        //                        { "redirect_uri", redirect_uri },//_config["AWSCognitoIntegration:redirect_uri"]},
        //                        { "code",Code}
        //                    });
        //                _logger.LogError(new EventId(500), "Access Token request :- " + request.ToString());
        //                var APIresponse = await httpClient.SendAsync(request);

        //                var resultContent = await APIresponse.Content.ReadAsStringAsync();
        //                _logger.LogError(new EventId(500), "Access Token response :- " + resultContent);
        //                if (APIresponse.StatusCode == System.Net.HttpStatusCode.OK)
        //                {
        //                    var key = JsonConvert.DeserializeObject<TokenDetails>(resultContent);
        //                    tokenDetails = key;

        //                    //response.ResponseObject = key;
        //                    //response.ResponseMessage = ResponseMessages[200];
        //                    //response.ResponseCode = ResponseCode.Success;
        //                    //return response;
        //                }
        //                //else
        //                //{
        //                //    response.ResponseObject = null;
        //                //    response.ResponseMessage = ResponseMessages[404];
        //                //    response.ResponseCode = ResponseCode.NotFound;
        //                //    return response;
        //                //}
        //            }
        //        };


        //        using (var httpClient1 = new HttpClient())
        //        {
        //            //using (var request1 = new HttpRequestMessage(new HttpMethod("POST"), "https://backend.prominencemabroker.com/api/v1/oauth2/token_info/"))
        //            using (var request1 = new HttpRequestMessage(new HttpMethod("POST"), "https://backend-stg.prominencemabroker.com/api/v1/oauth2/token_info/"))
        //            {
        //                request1.Headers.TryAddWithoutValidation("Content-Type", "application/json");
        //                request1.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        //                    {
        //                        { "redirect_uri", redirect_uri },//_config["AWSCognitoIntegration:redirect_uri"]},
        //                        { "code",Code}
        //                    });
        //                _logger.LogError(new EventId(500), "Token Info request :- " + request1.ToString());
        //                var APIresponse1 = await httpClient1.SendAsync(request1);

        //                var resultContent1 = await APIresponse1.Content.ReadAsStringAsync();
        //                _logger.LogError(new EventId(500), "Token Info Response :- " + resultContent1);
        //                if (APIresponse1.StatusCode == System.Net.HttpStatusCode.OK)
        //                {
        //                    var key = JsonConvert.DeserializeObject<TokenInfoAPI>(resultContent1);
        //                    tokenInfoAPI = key;
        //                    //response.ResponseObject = key;
        //                    //response.ResponseMessage = ResponseMessages[200];
        //                    //response.ResponseCode = ResponseCode.Success;
        //                    //return response;
        //                }
        //                else
        //                {
        //                    response.ResponseObject = null;
        //                    response.ResponseMessage = ResponseMessages[404];
        //                    response.ResponseCode = ResponseCode.NotFound;
        //                    return response;
        //                }
        //            }
        //        }

        //        tokenResponse.tokenInfo = tokenInfoAPI;
        //        tokenResponse.tokenDetails = tokenDetails;

        //        response.ResponseObject = tokenResponse;
        //        response.ResponseMessage = ResponseMessages[200];
        //        response.ResponseCode = ResponseCode.Success;
        //        return response;
        //    }

        //    catch (Exception ex)
        //    {
        //        response.ResponseObject = null;
        //        response.ResponseMessage = ResponseMessages[500];
        //        response.ResponseCode = ResponseCode.InterrnalError;
        //        _logger.LogError(new EventId(500), ex, "Error while processing request {0}", ex);
        //        throw;
        //    }
        //}

        // public async Task<ResponseModel<UserListResponse>> GetStagingUsersList(string token)
        public async Task<ResponseModel<UserListResponse>> GetStagingUsersList()
        {
            ResponseModel<UserListResponse> response = new();
            UserListResponse userListResponse = new UserListResponse();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // using (var request = new HttpRequestMessage(new HttpMethod("GET"),"https://backend-stg.prominencemabroker.com/api/v1/users-list/"))
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), _config["UserList"]))
                    {

                        //request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + token);
                        //request.Headers.TryAddWithoutValidation("Content-Type", "application/json");

                        var APIresponse = await httpClient.SendAsync(request);

                        var resultContent = await APIresponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                        _logger.LogError(new EventId(500), resultContent);
                        if (APIresponse.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            userListResponse.userListDetails = JsonConvert.DeserializeObject<List<UserListDetail>>(resultContent);
                            DataTable dt = new DataTable();
                            dt = JsonConvert.DeserializeObject<DataTable>(resultContent);
                            string conn = _config.GetConnectionString("DefaultConnection").ToString();
                            using (SqlConnection con = new SqlConnection(conn))
                            {
                                SqlCommand cmd = new SqlCommand("InsertUpdateUserList", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@tvp1", dt);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }

                            response.ResponseObject = userListResponse; // key;
                            response.ResponseMessage = ResponseMessages[200];
                            response.ResponseCode = ResponseCode.Success;
                            return response;
                        }
                        else
                        {
                            response.ResponseObject = null;
                            response.ResponseMessage = ResponseMessages[404];
                            response.ResponseCode = ResponseCode.NotFound;
                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(500), ex, "Error while processing request {0}", ex);
                response.ResponseObject = null;
                response.ResponseMessage = ResponseMessages[500];
                response.ResponseCode = ResponseCode.InterrnalError;

                throw;
            }
        }


        //public async Task<ResponseModel<UserListResponse>> GetProductionUsersList(string token)
        //{
        //    ResponseModel<UserListResponse> response = new();
        //    try
        //    {
        //        using (var httpClient = new HttpClient())
        //        {
        //            using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://backend-stg.prominencemabroker.com/api/v1/users-list/"))
        //            {

        //                request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + token);
        //                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");

        //                var APIresponse = await httpClient.SendAsync(request);

        //                var resultContent = await APIresponse.Content.ReadAsStringAsync().ConfigureAwait(false);
        //                if (APIresponse.StatusCode == System.Net.HttpStatusCode.OK)
        //                {
        //                    var key = JsonConvert.DeserializeObject<UserListResponse>(resultContent);
        //                    response.ResponseObject = key;
        //                    response.ResponseMessage = ResponseMessages[200];
        //                    response.ResponseCode = ResponseCode.Success;
        //                    return response;
        //                }
        //                else
        //                {
        //                    response.ResponseObject = null;
        //                    response.ResponseMessage = ResponseMessages[404];
        //                    response.ResponseCode = ResponseCode.NotFound;
        //                    return response;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.ResponseObject = null;
        //        response.ResponseMessage = ResponseMessages[500];
        //        response.ResponseCode = ResponseCode.InterrnalError;
        //        _logger.LogError(new EventId(500), ex, "Error while processing request {0}", ex);
        //        throw;
        //    }
        //}

    }
}
