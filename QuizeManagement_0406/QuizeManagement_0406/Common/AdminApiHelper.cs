using Newtonsoft.Json;
using QuizeManagement.Helpers.Helper;
using QuizeManagement.Models.DbContext;
using QuizeManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace QuizeManagement_0406.Common
{
    public class AdminApiHelper
    {
        public async Task<List<AllQuiezModel>> GetAllQuizzesAsync()
        {
            List<AllQuiezModel> list = new List<AllQuiezModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55656/api/Admin/");
                var response = await client.GetAsync("GetAllQuiez");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<AllQuiezModel>>(jsonString);
                }
            }
            return list;
        }  
        public async Task<List<QuizzeModel>> PostAddQuiezAsync(QuizzeModel quizzeModels)
        {
            List<QuizzeModel> list = new List<QuizzeModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55656/api/Admin/");
                string content = JsonConvert.SerializeObject(quizzeModels);              
                HttpResponseMessage response = await client.PostAsync("PostCreate", new StringContent(content, System.Text.Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<QuizzeModel>>(jsonString);
                }
            }
            return list;
        }
        public async void DeleteQuizzesAsync(int id)
        {
      
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55656/api/Admin/");
                string content = JsonConvert.SerializeObject(id);
                HttpResponseMessage response = await client.PostAsync("PostDelete", new StringContent(content, System.Text.Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    //list = JsonConvert.DeserializeObject<List<QuizzeModel>>(jsonString);
                }

                    
                }
                //return list;
            }
            
        }

    }
