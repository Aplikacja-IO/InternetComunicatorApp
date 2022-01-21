using InternetMessengerApp.DomainModels;
using InternetMessengerApp.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMessengerApp.Models
{
    public class MyProfileModel
    {
        private string token;
        private int userId;
        public RegisterUser LoggedUser { get; set; }
        public MyProfileModel(int _userId, string _token)
        {
            token = _token;
        }
        public async Task SetLoggedUser()
        {
            var server = new ServerAPIServices();
            LoggedUser = await server.GetUserById(userId, token);
        }
    }
}
