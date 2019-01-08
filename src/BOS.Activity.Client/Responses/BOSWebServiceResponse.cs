using BOS.Activity.Client.ClientModels;
using System.Collections.Generic;
using System.Net;

namespace BOS.Activity.Client.Responses
{
    public class BOSWebServiceResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<ActivityError> Errors { get; set; }

        public BOSWebServiceResponse(HttpStatusCode statusCode)
        {
            Errors = new List<ActivityError>();
            StatusCode = statusCode;
        }

        public bool IsSuccessStatusCode { get { return (int)StatusCode > 199 && (int)StatusCode < 300; } }
    }
}
