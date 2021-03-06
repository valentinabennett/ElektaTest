using ElektaTest.Infrastructure;
using System;
using System.Threading.Tasks;

namespace ElektaTest.Domain.Commands
{
    public class AddAppointmentCommand : ICommand
    {
        public Guid PatientId { get; }
        public DateTime AppointmentTime { get; }
        public int EquipmentId { get; }
        public bool Cancelled { get; }

        public AddAppointmentCommand(Guid patientId, DateTime appointmentTime, int equipmentId)
        {
            PatientId = patientId;
            AppointmentTime = appointmentTime;
            EquipmentId = equipmentId;
            Cancelled = false;
        }
    }

    public class AddTaskCommandHandler : ICommandHandler<AddAppointmentCommand>
    {
        private readonly AppointmentContext _context;

        public AddTaskCommandHandler(AppointmentContext context)
        {
            _context = context;
        }

        public async Task Handle(AddAppointmentCommand command)
        {

            var appointment = new Appointment { PatientId = command.PatientId, AppointmentTime = command.AppointmentTime,
                EequipmentId = command.EquipmentId, Canceled = command.Cancelled };
            await _context.Appointments.AddAsync(appointment);

            await _context.SaveChangesAsync();
        }
    }
}
