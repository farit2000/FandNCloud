using System;
using System.Threading.Tasks;
using FandNCloud.Common.RestEasy;
using RestEase;

namespace FandNCloud.Api.Services
{
    public interface IBasketActivitiesService
    {
        [Get("BasketActivities/file/{userId}/{path}/{name}")]
        [AllowAnyStatusCode]
        Task<BasketFileMessageModel> GetFileAsync([Path]string path, [Path]string name, [Path]Guid userId);
        
        [Get("BasketActivities/folder/{userId}/{path}/{name}")]
        [AllowAnyStatusCode]
        Task<BasketFolderMessageModel> GetFolderAsync([Path]string path, [Path]string name, [Path]Guid userId);

        [Get("products")]
        [AllowAnyStatusCode]
        Task<object> BrowseAsync();
    }
}