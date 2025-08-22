using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculo_ductos_winUi_3.Models;
using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;

namespace Calculo_ductos_winUi_3.Services
{ 
    public class WebApi
    {
        HttpClient _httpClient;
        private readonly string _baseUrl;

        public WebApi(string baseUrl)
        {
            _baseUrl = baseUrl.TrimEnd('/');
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(100);
        }

        //public async Task<T> GetAsync<T>(string endpoint)
        //{
        //    var response = await _httpClient.GetAsync($"{_baseUrl}/{endpoint}");
        //    response.EnsureSuccessStatusCode();

        //    var json = await response.Content.ReadAsStringAsync();
        //    return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //}
        public async Task<T> GetAsync<T>(string endpoint)
        {
            //var response = await _httpClient.GetAsync($"{_baseUrl}/{endpoint}");
            //response.EnsureSuccessStatusCode();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/{endpoint}");
            
            var response = await _httpClient.SendAsync(request);
            
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            var responseWrapper = JsonConvert.DeserializeObject<Response<ResultData<T>>>(json);
            
            // Deserialize into the wrapper structure
            //var responseWrapper = JsonSerializer.<Response<ResultData<T>>>(json);

            if (responseWrapper == null || responseWrapper.Result == null)
                throw new Exception("Respuesta inválida del servidor.");

            return responseWrapper.Result.Data;
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/{endpoint}", content);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseWrapper = JsonConvert.DeserializeObject<Response<ResultData<TResponse>>>(responseJson);

            //return JsonConvert.DeserializeObject<TResponse>(responseJson);
            return responseWrapper.Result.Data;
        }
    }
    

}
