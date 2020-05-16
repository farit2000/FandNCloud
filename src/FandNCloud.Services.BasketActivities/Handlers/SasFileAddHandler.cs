using System.Threading.Tasks;
using FandNCloud.Common.Requests;
using FandNCloud.Common.Responds;
using FandNCloud.Services.BasketActivities.Services;
using Microsoft.Extensions.Logging;

namespace FandNCloud.Services.BasketActivities.Handlers
{
    public class SasFileAddHandler : IRequestHandler<SasFileAddRequest>
    {
        private readonly IBlobService _blobService;
        private readonly ILogger _logger;

        public SasFileAddHandler(IBlobService blobService, ILogger<SasFileAddHandler> logger)
        {
            _logger = logger;
            _blobService = blobService;
        }
        public async Task<IRespond> HandleAsync(SasFileAddRequest request)
        {
            var url = await _blobService.GetBlobSasUriAddAsync(request.ContainerName, request.ContainerBlobName);
            return new SasFileAddRespond(request.ContainerName, request.ContainerBlobName, url);
        }
    }
}