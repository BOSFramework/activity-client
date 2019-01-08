using BOS.Activity.Client.ClientModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BOS.Activity.Client.Responses
{
    public class GetActivityByIdResponse<T> : BOSWebServiceResponse where T : IActivity
    {
        public T Activity { get; set; }

        public GetActivityByIdResponse(HttpStatusCode statusCode) 
            : base(statusCode)
        {
        }
    }
}
