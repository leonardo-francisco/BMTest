using BMWeb.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BMWeb.Services
{
    public class TravelRouteAPIService
    {
        private readonly HttpClient _apiClient;
        IConfiguration _configuration;

        public TravelRouteAPIService(HttpClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _configuration = configuration;
        }

        public async Task<List<TravelRouteDTO>> GetAllRoutes()
        {
            var uri = Convert.ToString(_configuration.GetValue<string>("API:TravelRouteUrl"));
            var response = _apiClient.GetAsync(uri);
            var result = response.Result;
            var resultado = await result.Content.ReadAsAsync<List<TravelRouteDTO>>();
            return resultado;
        }

        public async Task<TravelRouteDTO> GetRouteById(int id)
        {
            var uri = Convert.ToString(_configuration.GetValue<string>("API:TravelRouteUrl"));
            var response = _apiClient.GetAsync(string.Format(uri + "/{0}", id));
            var result = response.Result;
            var resultado = await result.Content.ReadAsAsync<TravelRouteDTO>();
            return resultado;
        }

        public async Task CreateRoute(TravelRouteDTO travelRouteDTO)
        {
            var uri = Convert.ToString(_configuration.GetValue<string>("API:TravelRouteUrl"));
            var rentContent = new StringContent(JsonConvert.SerializeObject(travelRouteDTO),
                                      System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, rentContent);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateRoute(TravelRouteDTO travelRouteDTO)
        {
            var uri = Convert.ToString(_configuration.GetValue<string>("API:TravelRouteUrl"));
            var rentContent = new StringContent(JsonConvert.SerializeObject(travelRouteDTO),
                                      System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PatchAsync(uri, rentContent);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteRoute(int id)
        {
            var uri = Convert.ToString(_configuration.GetValue<string>("API:TravelRouteUrl"));
            var response = _apiClient.DeleteAsync(string.Format(uri + "?id={0}", id));
            var result = response.Result;
            result.EnsureSuccessStatusCode();
        }

        public async Task<List<CheapestDistanceDTO>> CheapestRoute(string origem, string destino)
        {
            //var uri = Convert.ToString(_configuration.GetValue<string>("API:TravelRouteUrl"));
            var uri = "https://localhost:44300";
            var value = new Dictionary<string, string>
            {
                { "origem",origem },
                { "destino",destino }
            };
            var rentContent = new FormUrlEncodedContent(value);
            var response = await _apiClient.PostAsync(uri + "/CheapestRoute/" + origem+"/"+destino, rentContent);
            var result = response.Content.ReadAsAsync<List<CheapestDistanceDTO>>().Result;      
            return result;
        }
    }
}
