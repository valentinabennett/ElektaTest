using System;

namespace ElektaTest.Contracts.Requests
{
    public class CancelAppointmentRequest
    {
        public int EquipmentId { get; set; }

        public DateTime AppointmentTime { get; set; }
    }
}
