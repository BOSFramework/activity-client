using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BOS.Activity.Client.Responses
{
    public class DeleteActivityByIdResponse : BOSWebServiceResponse
    {
        public DeleteActivityByIdResponse(HttpStatusCode statusCode)
            : base(statusCode)
        {
        }
    }
}
