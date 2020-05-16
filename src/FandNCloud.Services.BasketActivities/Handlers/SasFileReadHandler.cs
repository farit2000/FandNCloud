using System.Threading.Tasks;
using FandNCloud.Common.Requests;
using FandNCloud.Common.Responds;
using FandNCloud.Services.BasketActivities.Services;
using Microsoft.Extensions.Logging;

namespace FandNCloud.Services.BasketActivities.Handlers
{
    public class SasFileReadHandler : IRequestHandler<SasFileReadRequest>
    {
        private readonly IBlobService _blobService;
        private readonly ILogger _logger;
        
        public SasFileReadHandler(IBlobService blobService, ILogger<SasFileReadHandler> logger)
        {
            _logger = logger;
            _blobService = blobService;
        }
        public async Task<IRespond> HandleAsync(SasFileReadRequest request)
        {
            var url = await _blobService.GetBlobSasUriReadAsync(request.ContainerName, request.ContainerBlobName);
            return new SasFileReadRespond(request.ContainerName, request.ContainerBlobName, url);
        }
    }
}