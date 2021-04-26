using ElektaTest.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ElektaTest.Services
{
    public class EquipmentAvailabilityNotification : IEquipmentAvailabilityNotification
    {
        private readonly ILogger<EquipmentAvailabilityNotification> _logger;

        public EquipmentAvailabilityNotification(ILogger<EquipmentAvailabilityNotification> logger)
        {
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
            // send async message to service bus 
            return Task.CompletedTask;
        }
    }
}
