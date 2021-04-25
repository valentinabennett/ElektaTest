using ElektaTest.Contracts.Requests;
using FluentValidation;
using System;

namespace ElektaTest.Validations
{
    public class CancelAppointmentRequestValidation : AbstractValidator<CancelAppointmentRequest>
    {
        public static readonly string NoPatientIdErrorMessage = "PatientId is required";
        public static readonly string AppointmentDateTimeThreeDaysBeforeErrorMessage = "Appointment can be cancelled up to 3 days before the appointment date";

        public CancelAppointmentRequestValidation()
        {
            RuleFor(x => x.PatientId).NotEmpty().WithMessage(NoPatientIdErrorMessage);
            RuleFor(x => x.AppointmentTime)
                  .GreaterThanOrEqualTo(DateTime.Now.Date.AddDays(3)).WithMessage(AppointmentDateTimeThreeDaysBeforeErrorMessage);
        }

    }
}



