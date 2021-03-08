using System.Collections.Generic;
using System.Threading.Tasks;
using BikingUltimate.Client.Model;
using ConsoleClient.Model;
using Refit;

namespace BikingUltimate.Client
{
    public interface IBikingService
    {
        [Get("/Bikes/{bikeId}/components")]
        Task<ICollection<Component>> GetComponents(int bikeId);

        [Post("/Bikes/{bikeId}/components")]
        Task<int> CreateComponent(int bikeId, ComponentInfo componentInfo);

        [Get("/Users")]
        Task<ICollection<User>> GetUsers();

        Task<ICollection<Bike>> GetBikes(int userId);
    }
}