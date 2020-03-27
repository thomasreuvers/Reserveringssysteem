using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using ReserveringsSysteemGui.Models;
using ReserveringsSysteemGui.Models.API_Models;

namespace ReserveringsSysteemGui.Handlers
{
    class ApiHandler
    {
        private readonly HttpClient _client;
        private readonly Uri _baseAddress = new Uri("https://localhost:44381");

        public ApiHandler()
        {
            _client = new HttpClient {BaseAddress = _baseAddress};
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        // POST: /api/user/login
        public async Task<string> LoginUserAsyncTask(LoginModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync("/api/user/login", content);
                _client.Dispose();
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                _client.Dispose();
                return null;
            }
        }

        // POST: /api/user/register
        public async Task<string> RegisterUserAsyncTask(RegisterModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync("/api/user/register", content);
                _client.Dispose();
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                _client.Dispose();
                return null;
            }
        }


    }
}
