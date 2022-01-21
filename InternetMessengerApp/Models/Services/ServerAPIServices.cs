using InternetMessengerApp.DomainModels;
using InternetMessengerApp.Models.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace InternetMessengerApp.Models.Services
{
    public class ServerAPIServices
    {

        string baseUrl = "https://localhost:44369/"; //tymczasowe, tutaj idzie adres serwera
        private HttpClient client;

      
        public ServerAPIServices()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
        }

        public async Task<string> GetUserJWTToken(UserInfo userInfo)
        {
            string jsonUserString = JsonSerializer.Serialize(userInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonUserString);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json'"));
            HttpResponseMessage responseMessage = await client.PostAsync("/api/Token", byteContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                var token = responseMessage.Content.ReadAsStringAsync().Result;
                return token;
            }
            return "Nieprawidlowe poswiadczenia";
        }
        public async Task<List<Group>> GetAllUserGroupsAsync(int userId, string token)
        {
            SetAuthorization(token);
            HttpResponseMessage responseMessage = await client.GetAsync($"/api/Group/AllUserGroups/{userId}");

            var jsonObjectGroups = responseMessage.Content.ReadAsStringAsync().Result;

            var groups = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Group>>(jsonObjectGroups);
            return groups;
        }
        public async Task<Post> GetPostById(int postId, string token)
        {
            SetAuthorization(token);
            var responseMessage = await client.GetAsync($"/api/Post/GetPostById/{postId}");

            var jsonObjectPost = responseMessage.Content.ReadAsStringAsync().Result;

            var post = Newtonsoft.Json.JsonConvert.DeserializeObject<Post>(jsonObjectPost);
            return post;
        }
        public async Task<RegisterUser> GetUserById(int userId, string token)
        {
            SetAuthorization(token);
            var responseMessage = await client.GetAsync($"/api/RegisterUser/{userId}");

            var jsonObjectUser = responseMessage.Content.ReadAsStringAsync().Result;

            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<RegisterUser>(jsonObjectUser);
            return user;
        }

        private void SetAuthorization(string token)
        {
            client.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
