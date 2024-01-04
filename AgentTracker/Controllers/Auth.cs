using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AgentTracker.Controllers
{
    public class Auth : Controller
    {
        private readonly IConfiguration _configuration;

        public Auth(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //[Route("GetToken")]
        //public async Task<IActionResult> GetToken(string Code)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://backend.prominencemabroker.com/api/v1/oauth2/token_info/"))
        //        {
        //            request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
        //            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        //                    {
        //                        { "redirect_uri", _configuration["AWSCognitoIntegration:redirect_uri"]},
        //                        { "code",Code}
        //                    });

        //            var response = await httpClient.SendAsync(request);

        //            var resultContent = await response.Content.ReadAsStringAsync();

        //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                var key = JsonConvert.DeserializeObject<System.Dynamic.ExpandoObject>(resultContent) as dynamic;
        //                return Json(key);
        //            }
        //            else
        //            {
        //                var key = resultContent;
        //                return Json(key);
        //            }
        //        }
        //    }
        //}
    }
}