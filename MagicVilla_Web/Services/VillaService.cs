using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {

        private readonly IHttpClientFactory _httpClient;
        private string villaUrl;

        public VillaService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI"); 
        }

        public Task<T> CreateAsync<T>(VillaCreateDTO dto, string token)
        {
            APIRequest ApiRequest = new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = villaUrl+"/api/VillaAPI",
                Token = token
            };
            return SendAsync<T>(ApiRequest);
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            APIRequest ApiRequest = new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villaUrl + "/api/VillaAPI/"+id,
                Token = token
            };
            return SendAsync<T>(ApiRequest);
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            APIRequest ApiRequest = new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaAPI",
                Token = token
            };
            return SendAsync<T>(ApiRequest);
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            APIRequest ApiRequest = new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaAPI/"+id,
                Token = token

            };
            return SendAsync<T>(ApiRequest);
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO dto, string token)
        {
            APIRequest ApiRequest = new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "/api/VillaAPI/"+dto.Id,
                Token = token
            };
            return SendAsync<T>(ApiRequest);
        }
    }
}
