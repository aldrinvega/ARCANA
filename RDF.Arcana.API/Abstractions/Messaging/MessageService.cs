using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RDF.Arcana.API.Abstractions.Messaging;

namespace RDF.Arcana.API.Services.Messaging
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public MessageService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            // Configure JSON serialization options
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<bool> SendMessageAsync(MessageRequest message)
        {
            var apiEndpoint = "https://sms-api.rdfmis.com/api/post_message";
            var apiKey = _configuration.GetValue<string>("SMS:token");

            // Set Authorization header with Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var smsRequest = new
            {
                system_name = "Arcana",
                message = message.Message,
                mobile_number = message.MobileNumber
            };

            var json = JsonSerializer.Serialize(smsRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiEndpoint, data);

            // Handle response and return success status
            return response.IsSuccessStatusCode;
        }
    }
}
