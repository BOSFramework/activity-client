using BOS.Activity.Client.ClientModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BOS.Activity.Client.Responses
{
    public class AddActivityResponse<T> : BOSWebServiceResponse where T : IActivity
    {
        public T Activity { get; set; }

        public AddActivityResponse(HttpStatusCode statusCode)
            : base(statusCode)
        {
        }
    }
}
