using System.Threading.Tasks;

namespace FandNCloud.Common.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}