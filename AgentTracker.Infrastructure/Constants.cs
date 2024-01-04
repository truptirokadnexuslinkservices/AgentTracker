using Microsoft.Extensions.Configuration;

namespace AgentTracker
{
    public class Constants
    {
        private readonly IConfiguration _configuration;
        public Constants(IConfiguration configuration)
        {
            _configuration = configuration;
        } 
        public static string RedAPITokenKey => "RedAPITokenKey";
        public const string RedCardAPIURL = "https://api.redcard.com/Source/ViewCard_V2.aspx?authtoken={0}&memberid={1}&maxMergeCount=3";
    }
}
