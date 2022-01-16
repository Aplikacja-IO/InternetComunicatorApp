using InternetMessengerApp.DomainModels;
using InternetMessengerApp.Models.Services;
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
        Dictionary<string, string> chatMessages;
        List<string> errorMessage;
        public ChatModel()
        {
            chatMessages = new Dictionary<string, string>();
            errorMessage = new List<string>();
        }
        private void CreateConnectionForUserWithToken(RegisterUser user, string tokenString)
        {
            var userId = user.UserId;
            connection = new HubConnectionBuilder()
                .WithUrl($"https://localhost:44369/ChatHub?userId={userId}", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(tokenString);
                })
                .Build();
            SetMethod(tokenString);
        }
        private async void SetMethod(string tokenString)
        {
            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
            connection.On<string, int>("NewMessageNotify", async (authorUserName, newPostId) =>
            {
                var server = new ServerAPIServices();
                var newPost = await server.GetPostById(newPostId, tokenString);
                chatMessages.Add(authorUserName, newPost.PostText);
            });
            try
            {
                await connection.StartAsync();

            }
            catch (Exception ex)
            {
                errorMessage.Add(ex.Message);
            }
        }
        public async void SendMessage(string fromAuthorIdString, string toParentGroupIdString, string postText)
        {
            try
            {
                await connection.InvokeAsync("SendPost",
                    fromAuthorIdString, toParentGroupIdString, postText);
            }
            catch (Exception e)
            {
                errorMessage.Add(e.Message);
            }
        }
    }
}
