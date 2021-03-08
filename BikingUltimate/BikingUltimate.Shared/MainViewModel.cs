using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using BikingUltimate.Client;
using BikingUltimate.Client.Model;
using ConsoleClient.Model;
using ReactiveUI;

namespace BikingUltimate
{
    public class MainViewModel : ReactiveObject
    {
        private ObservableAsPropertyHelper<ICollection<User>> users;
        private ObservableAsPropertyHelper<ICollection<Bike>> bikes;

        public MainViewModel(IBikingService bikingService)
        {
            LoadUsers = ReactiveCommand.CreateFromTask(bikingService.GetUsers);
            users = LoadUsers.ToProperty(this, model => model.Users);
            bikes = this.WhenAnyValue(model => model.SelectedUser)
                .SelectMany(u => bikingService.GetBikes(u.Id))
                .ToProperty(this, model => model.Bikes);
        }

        public ICollection<Bike> Bikes => bikes.Value;
        public ICollection<User> Users => users.Value;

        public ReactiveCommand<Unit, ICollection<User>> LoadUsers { get; }

        public User SelectedUser
        {
            get;
            set;
        }
    }
}