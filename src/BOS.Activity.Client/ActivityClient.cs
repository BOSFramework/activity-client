using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BOS.Activity.Client.ClientModels;
using BOS.Activity.Client.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace BOS.Activity.Client
{
    public class ActivityClient : IActivityClient
    {
        private readonly HttpClient _httpClient;
        private readonly DefaultContractResolver _contractResolver;

        public ActivityClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
        }

        /// <summary>
        /// Adds the provided activity.
        /// </summary>
        /// <typeparam name="T">Your implementation of IActivity.</typeparam>
        /// <param name="activity">The activity to add.</param>
        /// <returns></returns>
        public async Task<AddActivityResponse<T>> AddActivityAsync<T>(IActivity activity) where T : IActivity
        {
            var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_httpClient.BaseAddress}Activities?api-version=1.0");
            request.Content = new StringContent(JsonConvert.SerializeObject(activity, 
                new JsonSerializerSettings() { ContractResolver = _contractResolver, Formatting = Formatting.Indented }), 
                Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var addActivityresponse = new AddActivityResponse<T>(response.StatusCode);

            if (!addActivityresponse.IsSuccessStatusCode)
            {
                addActivityresponse.Errors.Add(new ActivityError((int)addActivityresponse.StatusCode));
                return addActivityresponse;
            }

            addActivityresponse.Activity = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            return addActivityresponse;
        }

        /// <summary>
        /// Delete the activity with the provided Id.
        /// </summary>
        /// <param name="activityId">The Id of the activity.</param>
        /// <returns></returns>
        public async Task<DeleteActivityByIdResponse> DeleteActivityByIdAsync(Guid activityId)
        {
            var response = await _httpClient.DeleteAsync($"Activities({activityId.ToString()})?api-version=1.0").ConfigureAwait(false);
            return new DeleteActivityByIdResponse(response.StatusCode);
        }

        /// <summary>
        /// Gets all of the activities.
        /// </summary>
        /// <typeparam name="T">Your implementation of IActivity</typeparam>
        /// <param name="filterDeleted"></param>
        /// <returns></returns>
        public async Task<GetActivitiesResponse<T>> GetActivitiesAsync<T>(bool filterDeleted = true) where T : IActivity
        {
            string queryString = filterDeleted ? "Activities?$filter=Deleted eq false&api-version=1.0" : "Activities?api-version=1.0";
            var response = await _httpClient.GetAsync(queryString).ConfigureAwait(false);
            var getActivitiesResponse = new GetActivitiesResponse<T>(response.StatusCode);

            if (!getActivitiesResponse.IsSuccessStatusCode)
            {
                return getActivitiesResponse;
            }

            JObject value = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
            getActivitiesResponse.Activities = JsonConvert.DeserializeObject<List<T>>(value["value"].ToString());
            return getActivitiesResponse;
        }

        /// <summary>
        /// Gets all acivties with the specified action.
        /// </summary>
        /// <typeparam name="T">Your implementation of IActivity.</typeparam>
        /// <param name="action">The action to filter by.</param>
        /// <param name="filterDeleted">Whether to filter out previously deleted activities. Defaults to true.</param>
        /// <returns></returns>
        public async Task<GetActivitiesByActionResponse<T>> GetActivitiesByActionAsync<T>(string action, bool filterDeleted = true) where T : IActivity
        {
            string queryString = filterDeleted ? $"Activities?$filter=Action eq '{action}' and Deleted eq false&api-version=1.0" 
                : $"Activities?$filter=Action eq '{action}'&api-version=1.0";

            var response = await _httpClient.GetAsync(queryString).ConfigureAwait(false);
            var getActivitiesResponse = new GetActivitiesByActionResponse<T>(response.StatusCode);

            JObject value = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
            getActivitiesResponse.Activities = JsonConvert.DeserializeObject<List<T>>(value["value"].ToString());
            return getActivitiesResponse;
        }

        /// <summary>
        /// Gets all activities with the specified correlation Id.
        /// </summary>
        /// <typeparam name="T">Your implementation of IActivity.</typeparam>
        /// <param name="correlationId">The correlation Id to filter by.</param>
        /// <param name="filterDeleted">Whether to filter out previously deleted activities. Defaults to true.</param>
        /// <returns></returns>
        public async Task<GetActivitiesByCorrelationIdResponse<T>> GetActivitiesByCorrelationIdAsync<T>(string correlationId, bool filterDeleted = true) where T : IActivity
        {
            string queryString = filterDeleted ? $"Activities?$filter=CorrelationId eq '{correlationId}' and Deleted eq false&api-version=1.0"
                : $"Activities?$filter=CorrelationId eq '{correlationId}'&api-version=1.0";

            var response = await _httpClient.GetAsync(queryString).ConfigureAwait(false);
            var getActivitiesResponse = new GetActivitiesByCorrelationIdResponse<T>(response.StatusCode);

            JObject value = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
            getActivitiesResponse.Activities = JsonConvert.DeserializeObject<List<T>>(value["value"].ToString());
            return getActivitiesResponse;
        }

        /// <summary>
        /// Gets all activities with the specified subject.
        /// </summary>
        /// <typeparam name="T">Your implementation of IActivity</typeparam>
        /// <param name="subject">The subject to filter by.</param>
        /// <param name="filterDeleted">Whether to filter out previously deleted activities. Defaults to true.</param>
        /// <returns></returns>
        public async Task<GetActivitiesBySubjectResponse<T>> GetActivitiesBySubjectAsync<T>(string subject, bool filterDeleted = true) where T : IActivity
        {
            string queryString = filterDeleted ? $"Activities?$filter=Subject eq '{subject}' and Deleted eq false&api-version=1.0"
                : $"Activities?$filter=Subject eq '{subject}'&api-version=1.0";

            var response = await _httpClient.GetAsync(queryString).ConfigureAwait(false);
            var getActivitiesResponse = new GetActivitiesBySubjectResponse<T>(response.StatusCode);

            JObject value = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
            getActivitiesResponse.Activities = JsonConvert.DeserializeObject<List<T>>(value["value"].ToString());
            return getActivitiesResponse;
        }

        /// <summary>
        /// Gets all the activities with the specified target.
        /// </summary>
        /// <typeparam name="T">Your implementation of IActivity</typeparam>
        /// <param name="target">The target to filter by.</param>
        /// <param name="filterDeleted">Whether to filter out previously deleted activities. Defaults to true.</param>
        /// <returns></returns>
        public async Task<GetActivitiesByTargetResponse<T>> GetActivitiesByTargetAsync<T>(string target, bool filterDeleted = true) where T : IActivity
        {
            string queryString = filterDeleted ? $"Activities?$filter=Target eq '{target}' and Deleted eq false&api-version=1.0"
                : $"Activities?$filter=Target eq '{target}'&api-version=1.0";

            var response = await _httpClient.GetAsync(queryString).ConfigureAwait(false);
            var getActivitiesResponse = new GetActivitiesByTargetResponse<T>(response.StatusCode);

            JObject value = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
            getActivitiesResponse.Activities = JsonConvert.DeserializeObject<List<T>>(value["value"].ToString());
            return getActivitiesResponse;
        }

        /// <summary>
        /// Gets all the activities with the specified time stamp.
        /// </summary>
        /// <typeparam name="T">Your implementation of IActivity</typeparam>
        /// <param name="timeStamp">The time stamp to filter by.</param>
        /// <param name="filterDeleted">Whether to filter out previously deleted activities. Defaults to true.</param>
        /// <returns></returns>
        public async Task<GetActivitiesByTimeStampResponse<T>> GetActivitiesByTimeStampAsync<T>(string timeStamp, bool filterDeleted) where T : IActivity
        {
            string queryString = filterDeleted ? $"Activities?$filter=TimeStamp eq '{timeStamp}' and Deleted eq false&api-version=1.0"
                : $"Activities?$filter=TimeStamp eq '{timeStamp}'&api-version=1.0";

            var response = await _httpClient.GetAsync(queryString).ConfigureAwait(false);
            var getActivitiesResponse = new GetActivitiesByTimeStampResponse<T>(response.StatusCode);

            JObject value = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
            getActivitiesResponse.Activities = JsonConvert.DeserializeObject<List<T>>(value["value"].ToString());
            return getActivitiesResponse;
        }

        /// <summary>
        /// Gets all the activities with the specified source.
        /// </summary>
        /// <typeparam name="T">Your implementation of IActivity</typeparam>
        /// <param name="source">The source to filter by.</param>
        /// <param name="filterDeleted">Whether to filter out previously deleted activities. Defaults to true.</param>
        /// <returns></returns>
        public async Task<GetActivitiesBySourceResponse<T>> GetActivitiesBySourceAsync<T>(string source, bool filterDeleted = true) where T : IActivity
        {
            string queryString = filterDeleted ? $"Activities?$filter=Source eq '{source}' and Deleted eq false&api-version=1.0"
                : $"Activities?$filter=Source eq '{source}'&api-version=1.0";

            var response = await _httpClient.GetAsync(queryString).ConfigureAwait(false);
            var getActivitiesResponse = new GetActivitiesBySourceResponse<T>(response.StatusCode);

            JObject value = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
            getActivitiesResponse.Activities = JsonConvert.DeserializeObject<List<T>>(value["value"].ToString());
            return getActivitiesResponse;
        }

        /// <summary>
        /// Gets the activity with the specified Id.
        /// </summary>
        /// <typeparam name="T">Your implementation of IActivity.</typeparam>
        /// <param name="activityId">The Id of the Activity to retrieve.</param>
        /// <returns></returns>
        public async Task<GetActivityByIdResponse<T>> GetActivityByIdAsync<T>(Guid activityId) where T : IActivity
        {
            var response = await _httpClient.GetAsync($"Activities?$filter=Id eq {activityId.ToString()}&api-version=1.0").ConfigureAwait(false);
            var getActivitiesResponse = new GetActivityByIdResponse<T>(response.StatusCode);

            JObject value = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
            List<T> resp = JsonConvert.DeserializeObject<List<T>>(value["value"].ToString());
            getActivitiesResponse.Activity = resp.FirstOrDefault();
            return getActivitiesResponse;
        }
    }
}
