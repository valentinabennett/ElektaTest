using ElektaTest.Exceptions;
using ElektaTest.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ElektaTest.Domain.Commands
{
    public class CancelAppointmentCommand : ICommand
    {
        public Guid PatientId { get; }
        public DateTime AppointmentTime { get; }

        public CancelAppointmentCommand(Guid patientId, DateTime appointmentTime)
        {
            PatientId = patientId;
            AppointmentTime = appointmentTime;
        }
    }

    public class CancelAppointmentCommandHandler : ICommandHandler<CancelAppointmentCommand>
    {
        private readonly AppointmentContext _context;

        public CancelAppointmentCommandHandler(AppointmentContext context)
        {
            _context = context;
        }

        public async Task Handle(CancelAppointmentCommand command)
        {
            var appointment = await _context.Appointments
                .SingleOrDefaultAsync(x => x.PatientId == command.PatientId && x.AppointmentTime == command.AppointmentTime);

            if (appointment == null)
            {
                throw new AppointmentNotFoundException(command.PatientId);
            }

            appointment.Canceled = true;

            await _context.SaveChangesAsync();
        }
    }
}
