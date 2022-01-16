using InternetMessengerApp.DomainModels;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMessengerApp.Models
{
    public class ChatModel
    {
        HubConnection connetion;
        private void CreateConnectionForUserWithToken(RegisterUser user, string token)
        {
            Connection = new HubConnectionBuilder()
                .WithUrl($"https://localhost:44369/ChatHub?userId={userId}", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(tokenString);
                })
                .Build();
            connectionId = connection.ConnectionId;
        }
    }
}
