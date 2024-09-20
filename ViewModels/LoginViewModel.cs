using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using RestaurantManagementApp.Models;
using RestaurantManagementApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementApp.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        readonly ILoginRepository loginRepository = new LoginService();
        [RelayCommand]
        public async void Login()
        {
            if (!string.IsNullOrWhiteSpace(this._userName) && !string.IsNullOrWhiteSpace(this._password))
            {
                UserInfo userInfo = await loginRepository.Login(UserName, Password);
                if (Preferences.ContainsKey(nameof(App.UserInfo)))
                {
                    Preferences.Remove(nameof(App.UserInfo));
                }
                string userDetails = JsonConvert.SerializeObject(userInfo);
                Preferences.Set(nameof(App.UserInfo), userDetails);
                App.UserInfo = userInfo;
                await Shell.Current.GoToAsync("//HomePage");
            }
        }
    }
}
