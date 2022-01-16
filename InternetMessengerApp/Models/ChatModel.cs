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
        HubConnection connection;
        List<Component> chatMessages;
        List<string> errorMessage;
        
        private void CreateConnectionForUserWithToken(RegisterUser user, string tokenString)
        {
            connection = new HubConnectionBuilder()
                .WithUrl($"https://localhost:44369/ChatHub?userId={userId}", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(tokenString);
                })
                .Build();
        }
        private async void SetMethod()
        {
            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
            try
            {
                await connection.StartAsync();

            }
            catch (Exception ex)
            {
                errorMessage.Add(ex.Message);
            }
        }
    }
}
