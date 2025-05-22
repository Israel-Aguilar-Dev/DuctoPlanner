using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_ductos_winUi_3.Services
{
    using Calculo_ductos_winUi_3.Models;
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    namespace TuProyecto.Servicios
    {
        public class WebApi
        {
            private readonly HttpClient _httpClient;
            private readonly string _baseUrl;

            public WebApi(string baseUrl)
            {
                _baseUrl = baseUrl.TrimEnd('/');
                _httpClient = new HttpClient();
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
                var response = await _httpClient.GetAsync($"{_baseUrl}/{endpoint}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                // Deserialize into the wrapper structure
                var responseWrapper = JsonSerializer.Deserialize<Response<ResultData<T>>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (responseWrapper == null || responseWrapper.Result == null)
                    throw new Exception("Respuesta inválida del servidor.");

                return responseWrapper.Result.Data;
            }

            public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
            {
                var jsonData = JsonSerializer.Serialize(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/{endpoint}", content);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResponse>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }
    }

}
