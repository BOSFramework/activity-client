﻿using BOS.Activity.Client.ClientModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BOS.Activity.Client.Responses
{
    public class GetActivitiesResponse<T> : BOSWebServiceResponse where T : IActivity
    {
        public List<T> Activities { get; set; }

        public GetActivitiesResponse(HttpStatusCode statusCode)
            : base(statusCode)
        {
            Activities = new List<T>();
        }
    }
}
