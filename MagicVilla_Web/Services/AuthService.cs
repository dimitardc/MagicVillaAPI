using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class AuthService :BaseService, IAuthService
    {


        private readonly IHttpClientFactory _httpClient;
        private string villaUrl;

        public AuthService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> LoginAsync<T>(LoginRequestDTO obj)
        {
            APIRequest ApiRequest = new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "/api/Users/login"
            };
            return SendAsync<T>(ApiRequest);
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestDTO obj)
        {
            APIRequest ApiRequest = new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "/api/Users/register"
            };
            return SendAsync<T>(ApiRequest);
        }
    }
}
