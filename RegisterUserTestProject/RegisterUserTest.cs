using InternetMessengerApp.DomainModels;
using InternetMessengerApp.Models.Helpers;
using InternetMessengerApp.Models.Services;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace RegisterUserTestProject
{
    public class RegisterUserTest
    {
        private string token;
        [Test]
        public async Task LoginUserTest()
        {
            var user = new UserInfo()
            {
                userId = 2,
                username = "test1",
                password = "test1"
            };
            var server = new ServerAPIServices();
            var serverRespond = await server.GetUserJWTToken(user);
            if (serverRespond == "Nieprawidlowe poswiadczenia") Assert.Fail();
            else 
            {
                token = serverRespond;
                Assert.Pass();
            }
        }
        [Test]
        public async Task TestUserToken()
        {
            var userId = 2;
            var server = new ServerAPIServices();
            var currentUser = await server.GetUserById(userId, token);
            if (currentUser == null) Assert.Fail();
            else Assert.Pass();
        }
    }
   
}