using ElektaTest.Contracts.Requests;
using ElektaTest.Contracts.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ElektaTest.Services
{
    public class EquipmentAvailabilityService : IEquipmentAvailabilityService
    {
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

                // Assamption - http factory create client and send async request to api endpoint with http method GET  to get availibility info.

                var response = new EquipmentAvailabilityResponse { EequipmentId = 1, IsAvailable = true, Date = appointmentTime };

                return Task.FromResult(response);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error to get availability: {e.Message}");
                throw;
            }

        }

        public Task CancelAppointment(CancelAppointmentRequest cancelAppointmentRequest)
        {
            try
            {
                var client = _clientFactory.CreateClient();

                // Assamption - send async request to external endpoint to cancel the appointment with http method PUT
                //var dataJson = new StringContent(
                //    JsonSerializer.Serialize(cancelAppointmentRequest),
                //    Encoding.UTF8,
                //    "application/json");

                //using var httpResponse =
                //    await client.PutAsync($"/api/equipments/{cancelAppointmentRequest.EquipmentId}/cancelAppointment", dataJson);

                //httpResponse.EnsureSuccessStatusCode();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error to cancel appointment: {e.Message}");
                throw;
            }

        }

    }
}
