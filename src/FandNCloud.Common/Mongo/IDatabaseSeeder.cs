using System.Threading.Tasks;

namespace FandNCloud.Common.Mongo
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}