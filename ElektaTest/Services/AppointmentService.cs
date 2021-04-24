using ElektaTest.Commands;
using ElektaTest.Contracts.Requests;
using ElektaTest.Queries;
using System.Threading.Tasks;

namespace ElektaTest.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IQueryHandler _queryHandler;
        private readonly ICommandHandler _commandHandler;

        public AppointmentService(IQueryHandler queryHandler, ICommandHandler commandHandler)
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
        }
        public async Task AddAppointment(NewAppointmentRequest appointmentRequest, int equipmentId)
        {
            var command = new AddAppointmentCommand(appointmentRequest.PatientId, appointmentRequest.AppointmentTime, equipmentId, false);

            await _commandHandler.Handle(command);
        }
    }
}
