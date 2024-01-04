using System.Collections.Generic;
using System.Collections.Immutable;

namespace AgentTracker.API.Models
{
    public static class Responses
    {
        public enum ResponseCode
        {
            Success = 200,
            UnAuthorized = 401,
            Forbidden = 403,
            NotFound = 404,
            InterrnalError = 500,
            Failed = 508,
            ValidationFailure = 1,
            AlreadyExists = 2,
            InActiveUser = 3,
        } 

        public static readonly ImmutableDictionary<int, string> ResponseMessages = new Dictionary<int, string>()
        {
            { 200, "Success" },
            { 401 , "You are not Authorized to access this resource" },
            { 403 , "You are not Forbidden to access this resource" },
            { 404 , "The Resource You are looking for is not avilable" },
            { 500 , "Some error occoured. Please try again" },
            { 508 , "User old password is wrong. Please try again" },
            { 1, "Validation failure"},
            { 2, "Data Already Exists"},
            { 3, "Your account is in InActive state, please email us at phpemployerportal@uhsinc.com" }
        }.ToImmutableDictionary();
    }
}
