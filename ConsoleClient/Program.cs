using System;
using System.Threading.Tasks;
using BikingUltimate.Client;
using BikingUltimate.Client.Model;
using ConsoleClient.Model;

namespace ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = BikingUltimateClient.Create();
            await PrintComponents(client, 1);
            //await CreateComponents(client, 1);
            //await PrintUsers(client);
            //await PrintBikes(client, 1);
        }

        private static async Task PrintBikes(IBikingService client, int userId)
        {
            var bikes = await client.GetBikes(userId);

            foreach (var component in bikes)
            {
                Console.WriteLine(component);
            }

            Console.ReadLine();
        }

        private static async Task PrintUsers(IBikingService client)
        {
            var users = await client.GetUsers();
            foreach (var user in users)
            {
                Console.WriteLine(user);
            }

            Console.ReadLine();
        }

        private static async Task PrintComponents(IBikingService client, int bikeId)
        {
            var components = await client.GetComponents(bikeId);

            foreach (var component in components)
            {
                Console.WriteLine(component);
            }

            Console.ReadLine();
        }

        private static async Task CreateComponents(IBikingService client, int bikeId)
        {
            var componentInfo = new ComponentInfo()
            {
                ComponentType = ComponentType.Chain,
                AddedOn = DateTimeOffset.Now,
                Brand = "Niidea",
                Model = "Modelo",
                Distance = 0D,
                Weight = 0.4D,
            };

            var createdId = await client.CreateComponent(bikeId, componentInfo);

            Console.WriteLine($"Componente creado con el id {createdId}");
            Console.ReadLine();
        }
    }
}
