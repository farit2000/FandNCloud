using System.Threading.Tasks;
using FandNCloud.Common.Responds;

namespace FandNCloud.Common.Requests
{
    public interface IRequestHandler<in T> where T: IRequest
    {
        Task<IRespond> HandleAsync(T request);
    }
}