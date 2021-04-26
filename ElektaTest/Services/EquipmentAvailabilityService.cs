using ElektaTest.Contracts.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ElektaTest.Services
{
    public class EquipmentAvailabilityService : IEquipmentAvailabilityService
    {
        // 1. assamption - if  the equipment avaliability system has the endpoints that are exposed swagger (Open API Specification)
        //files  then we could generate the clients. And use the Client to call the system endpoints.

        // 2. Or use IHttpClientFactory to create client.


        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<EquipmentAvailabilityService> _logger;
        public EquipmentAvailabilityService(IHttpClientFactory clientFactory, ILogger<EquipmentAvailabilityService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
         
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
