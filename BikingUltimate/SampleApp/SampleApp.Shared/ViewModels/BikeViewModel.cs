using System.Collections.Generic;
using System.Reactive;
using BikingUltimate.Client;
using BikingUltimate.Client.Model;
using ConsoleClient.Model;
using ReactiveUI;

namespace SampleApp.ViewModels
{
    public class BikeViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<ICollection<Component>> components;
        private readonly Bike bike;

        public BikeViewModel(Bike bike, IBikingService bikingService)
        {
            this.bike = bike;

            LoadComponents = ReactiveCommand.CreateFromTask(() => bikingService.GetComponents(bike.Id));
            components = LoadComponents.ToProperty(this, model => model.Components);
        }

        public ICollection<Component> Components => components.Value;

        public ReactiveCommand<Unit, ICollection<Component>> LoadComponents { get; }

        public string Brand => bike.Brand;
        public string Model => bike.Model;
    }
}