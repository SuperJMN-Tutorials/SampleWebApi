using BikingUltimate.Client.Model;

namespace SampleApp.ViewModels
{
    public class UserViewModel
    {
        private readonly User user;

        public UserViewModel(User user)
        {
            this.user = user;
        }

        public string Username => user.Username;
        public string FirstName => user.FirstName;
        public string LastName => user.LastName;
    }
}