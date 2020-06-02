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
    public class ApiHandler
    {
        private readonly HttpClient _client;
        private readonly Uri _baseAddress = new Uri("https://localhost:44381");

        public ApiHandler()
        {
            _client = new HttpClient { BaseAddress = _baseAddress };
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

        // POST: /api/reservation/makereservation
        public async Task<string> MakeReservationTask(Reservation model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync("/api/reservation/makereservation", content);
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

        // POST: /api/reservation/getreservations
        public async Task<string> GetReservationsTask(string userCode)
        {
            var content = new StringContent(JsonConvert.SerializeObject(userCode), Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync("/api/reservation/getreservations", content);
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

        // GET: /api/reservation/getallreservations
        public async Task<string> GetAllReservationsTask()
        {
            try
            {
                var response = await _client.GetAsync("/api/reservation/getallreservations");
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

        // DELETE: /api/reservation/deletereservations
        public async Task DeleteReservations()
        {
            try
            {
                var response = await _client.DeleteAsync("/api/reservation/deletereservations");
                _client.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                _client.Dispose();
            }
        }

        // GET: /api/reservation/gettables
        public async Task<string> GetTablesTask()
        {
            try
            {
                var response = await _client.GetAsync("/api/reservation/gettables");
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

        // POST: /api/reservation/createtable
        public async Task<string> CreateTableTask(Table model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync("/api/reservation/createtable", content);
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
