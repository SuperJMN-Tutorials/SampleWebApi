using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using BikingUltimate.Client;
using BikingUltimate.Client.Model;
using ReactiveUI;

namespace SampleApp.ViewModels
{
    public class UsersViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<ICollection<UserViewModel>> users;
        private ObservableAsPropertyHelper<bool> isBusy;

        public UsersViewModel(IBikingService bikingService)
        {
            LoadUsers = ReactiveCommand.CreateFromTask(bikingService.GetUsers);
            users = LoadUsers
                .Select(us =>
                    us.Select(user => new UserViewModel(user)).ToList()).ToProperty(this, model => model.Users);
            
            isBusy = LoadUsers.IsExecuting.ToProperty(this, vm => vm.IsBusy);
        }

        public bool IsBusy => isBusy.Value;

        public ICollection<UserViewModel> Users => users.Value;

        public ReactiveCommand<Unit, ICollection<User>> LoadUsers { get; }

        public User SelectedUser
        {
            get;
            set;
        }
    }
}