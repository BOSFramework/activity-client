using BOS.Activity.Client.ClientModels;
using BOS.Activity.Client.Responses;
using System;
using System.Threading.Tasks;

namespace BOS.Activity.Client
{
    public interface IActivityClient
    {
        Task<AddActivityResponse<T>> AddActivityAsync<T>(IActivity activity) where T : IActivity;
        Task<GetActivityByIdResponse<T>> GetActivityByIdAsync<T>(Guid activityId) where T : IActivity;
        Task<GetActivitiesResponse<T>> GetActivitiesAsync<T>(bool filterDeleted = true) where T : IActivity;
        Task<GetActivitiesByCorrelationIdResponse<T>> GetActivitiesByCorrelationIdAsync<T>(string correlationId, bool filterDeleted = true) where T : IActivity;
        Task<GetActivitiesBySubjectResponse<T>> GetActivitiesBySubjectAsync<T>(string subject, bool filterDeleted = true) where T : IActivity;
        Task<GetActivitiesByTargetResponse<T>> GetActivitiesByTargetAsync<T>(string target, bool filterDeleted = true) where T : IActivity;
        Task<GetActivitiesBySourceResponse<T>> GetActivitiesBySourceAsync<T>(string source, bool filterDeleted = true) where T : IActivity;
        Task<GetActivitiesByTimeStampResponse<T>> GetActivitiesByTimeStampAsync<T>(string timeStamp, bool filterDeleted = true) where T : IActivity;
        Task<GetActivitiesByActionResponse<T>> GetActivitiesByActionAsync<T>(string action, bool filterDeleted = true) where T : IActivity;
        Task<DeleteActivityByIdResponse> DeleteActivityByIdAsync(Guid activityId);
    }
}
