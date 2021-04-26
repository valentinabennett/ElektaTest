using ElektaTest.Contracts.Requests;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ElektaTest.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private ILogger<EmailNotificationService> _logger;

        public EmailNotificationService(ILogger<EmailNotificationService> logger)
        {
            _logger = logger;
        }
        public Task SendBookingAppointmentNotificationEmail(NewAppointmentRequest appointment)
        {
            _logger.LogInformation($"Notification for booking an appointment sent to {appointment.PatientId} time: {appointment.AppointmentTime}");
            return Task.CompletedTask;
        }

        public Task SendNotAvailableNotificationEmail(NewAppointmentRequest appointment)
        {
            _logger.LogInformation($"Notification for not available appointment sent to {appointment.PatientId} time: {appointment.AppointmentTime}");

            return Task.CompletedTask;
        }
      
    }
}
