using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {

        private readonly IHttpClientFactory _httpClient;
        private string villaUrl;

        public VillaNumberService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaNumberCreatedDTO dto, string token)
        {
            APIRequest ApiRequest = new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = villaUrl+ "/api/VillaNumberAPI",
                Token = token
            };
            return SendAsync<T>(ApiRequest);
        }

        public Task<T> DeleteAsync<T>(int id,string token)
        {
            APIRequest ApiRequest = new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villaUrl + "/api/VillaNumberAPI/" + id,
                Token = token
            };
            return SendAsync<T>(ApiRequest);
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            APIRequest ApiRequest = new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaNumberAPI",
                Token = token
            };
            return SendAsync<T>(ApiRequest);
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            APIRequest ApiRequest = new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaNumberAPI/" + id,
                Token = token
            };
            return SendAsync<T>(ApiRequest);
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdatedDTO dto, string token)
        {
            APIRequest ApiRequest = new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "/api/VillaNumberAPI/" + dto.VillaNo,
                Token = token
            };
            return SendAsync<T>(ApiRequest);
        }
    }
}
