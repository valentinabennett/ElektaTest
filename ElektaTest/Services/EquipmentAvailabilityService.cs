using ElektaTest.Contracts.Responses;
using ElektaTest.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElektaTest.Services
{
    public class EquipmentAvailabilityService : IEquipmentAvailabilityService
    {
        // 1. assamption - if  the equipment avaliability system has the endpoints that are exposed swagger (Open API Specification)
        //files  then we could generate the clients. And use the Client to call the system endpoints.

        // 2. Or use IHttpClientFactory to create client.

        // 3. or for events messages could be used service bus

        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<EquipmentAvailabilityService> _logger;
        public EquipmentAvailabilityService(IHttpClientFactory clientFactory, ILogger<EquipmentAvailabilityService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;

            SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() },
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects
            };
            SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
        }

        public JsonSerializerSettings SerializerSettings { get; set; }

        public Task PublishMessageAsync(EventMessage eventMessage)
        {
            var jsonObjectString = JsonConvert.SerializeObject(eventMessage, SerializerSettings);
            var messageBytes = Encoding.UTF8.GetBytes(jsonObjectString);
            // send async
            return Task.CompletedTask;
        }

        public Task<EquipmentAvailabilityResponse> CheckAvailability(DateTime appointmentTime)
        {
            try
            {
                var client = _clientFactory.CreateClient();
                var response = new EquipmentAvailabilityResponse { EequipmentId = 1, IsAvailable = true, Date = appointmentTime };

                return Task.FromResult(response);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error to get availability: {e.Message}");
                throw;
            }
        }
    }
}
