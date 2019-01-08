using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;

namespace BOS.Activity.Client.ServiceExtension
{
    public static class ServiceConfiguration
    {
        public static void AddBOSActivityClient(this IServiceCollection services, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new NullReferenceException("BOS API Key must not be null or empty.");
            }

            services.AddHttpClient<IActivityClient, ActivityClient>(client =>
            {
                client.BaseAddress = new Uri("https://apis.dev.bosframework.com/activity/odata/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            });
        }
    }
}
