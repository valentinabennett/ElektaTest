using ElektaTest.Infrastructure;
using ElektaTest.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ElektaTest.Domain.Commands
{
    public class UpdateAppointmentCommand : ICommand
    {
        public Guid PatientId { get; }
        public DateTime AppointmentTime { get; }

        public DateTime NewAppointmentTime { get; }

        public UpdateAppointmentCommand(Guid patientId, DateTime appointmentTime, DateTime newAppointmentTime)
        {
            PatientId = patientId;
            AppointmentTime = appointmentTime;
            NewAppointmentTime = newAppointmentTime;
        }
    }

    public class UpdateAppointmentCommandHandler : ICommandHandler<UpdateAppointmentCommand>
    {
        private readonly AppointmentContext _context;

        public UpdateAppointmentCommandHandler(AppointmentContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateAppointmentCommand command)
        {
            var appointment = await _context.Appointments
                .SingleOrDefaultAsync(x => x.PatientId == command.PatientId && x.AppointmentTime == command.AppointmentTime);

            if (appointment == null)
            {
                throw new AppointmentNotFoundException(command.PatientId);
            }

            appointment.AppointmentTime = command.NewAppointmentTime;

            await _context.SaveChangesAsync();
        }
    }
}
