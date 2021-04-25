using System;

namespace ElektaTest.Exceptions
{
    public class AppointmentNotFoundException : Exception
    {
        public AppointmentNotFoundException(Guid patientId) : base($"Appointment for {patientId} does not exist")
        {
        }
    }
}
