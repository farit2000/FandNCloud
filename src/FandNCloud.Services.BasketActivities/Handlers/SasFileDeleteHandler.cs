using System.Threading.Tasks;
using FandNCloud.Common.Requests;
using FandNCloud.Common.Responds;
using FandNCloud.Services.BasketActivities.Services;
using Microsoft.Extensions.Logging;

namespace FandNCloud.Services.BasketActivities.Handlers
{
    public class SasFileDeleteHandler : IRequestHandler<SasFileDeleteRequest>
    {
        private readonly IBlobService _blobService;
        private readonly ILogger _logger;

        public SasFileDeleteHandler(IBlobService blobService, ILogger<SasFileDeleteHandler> logger)
        {
            _logger = logger;
            _blobService = blobService;
        }
        public async Task<IRespond> HandleAsync(SasFileDeleteRequest request)
        {
            var url = await _blobService.GetBlobSasUriDeleteAsync(request.ContainerName, request.ContainerBlobName);
            return new SasFileReadRespond(request.ContainerName, request.ContainerBlobName, url);
        }
    }
}