using ElektaTest.Contracts.Requests;
using System.Threading.Tasks;

namespace ElektaTest.Services
{
    public interface IAppointmentService
    {
        Task AddAppointment(NewAppointmentRequest appointmentRequest, int equipmentId);
    }
}
