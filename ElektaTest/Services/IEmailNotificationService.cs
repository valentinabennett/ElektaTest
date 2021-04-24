using ElektaTest.Contracts.Requests;
using System.Threading.Tasks;

namespace ElektaTest.Services
{
    public interface IEmailNotificationService
    {
        Task SendBookingAppointmentNotificationEmail(NewAppointmentRequest appointmen);

        Task SendNotAvailableNotificationEmail(NewAppointmentRequest appointmen);
    }
}
