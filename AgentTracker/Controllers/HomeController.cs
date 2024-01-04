using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AgentTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet("login")] 
        //public string Login()
        //{
        //    var redirectURL = "https://auth.prominencemabroker.com/login?" +
        //                     "&client_id=" + _configuration["AWSCognitoIntegration:client_id"].ToString() +
        //                     "&response_type=code" +
        //                     "&scope=" + _configuration["AWSCognitoIntegration:scope"].ToString() +
        //                     "&redirect_uri=" + _configuration["AWSCognitoIntegration:redirect_uri"].ToString();

        //    return redirectURL ;
        //}

    }
}
