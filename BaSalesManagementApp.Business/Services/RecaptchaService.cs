using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaSalesManagementApp.Business.Services
{
    public class RecaptchaService
    {
        private readonly RecaptchaSettings _recaptchaSettings;
        private readonly HttpClient _httpClient;

        public RecaptchaService(IOptions<RecaptchaSettings> recaptchaSettings, HttpClient httpClient)
        {
            _recaptchaSettings = recaptchaSettings.Value;
            _httpClient = httpClient;
        }

        public async Task<bool> Validate(string recaptchaResponse)
        {
            var response = await _httpClient.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={_recaptchaSettings.SecretKey}&response={recaptchaResponse}");
            var json = JObject.Parse(response);
            return json["success"].Value<bool>();
        }
    }
}
