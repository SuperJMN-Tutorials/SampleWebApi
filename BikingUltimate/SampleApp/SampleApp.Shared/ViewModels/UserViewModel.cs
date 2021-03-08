using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using BikingUltimate.Client;
using BikingUltimate.Client.Model;
using ReactiveUI;

namespace SampleApp.ViewModels
{
    public class UserViewModel : ReactiveObject
    {
        private readonly User user;
        private readonly ObservableAsPropertyHelper<ICollection<Bike>> bikes;
        private Bike selectedBike;

        public UserViewModel(User user, IBikingService bikingService)
        {
            this.user = user;
            LoadBikes = ReactiveCommand.CreateFromTask(() => bikingService.GetBikes(user.Id));
            bikes = LoadBikes.ToProperty(this, model => model.Bikes);
            this.WhenAnyValue(x => x.SelectedBike)
                .Where(x => x != null)
                .Subscribe(b => MessageBus.Current.SendMessage(b));
        }

        public ICollection<Bike> Bikes => bikes.Value;

        public ReactiveCommand<Unit, ICollection<Bike>> LoadBikes { get; }

        public string Username => user.Username;
        public string FirstName => user.FirstName;
        public string LastName => user.LastName;

        public Bike SelectedBike
        {
            get => selectedBike;
            set => this.RaiseAndSetIfChanged(ref selectedBike, value);
        }
    }
}