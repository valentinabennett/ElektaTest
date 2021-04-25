using ElektaTest.Contracts.Requests;
using FluentValidation;
using System;

namespace ElektaTest.Validations
{
    public class UpdateAppointmentRequestValidation : AbstractValidator<UpdateAppointmentRequest>
    {
        public static readonly string NoPatientIdErrorMessage = "PatientId is required";
        public static readonly string AppointmentDateTimeTwoDaysBeforeErrorMessage = "The new appointment can be made up to 2 days before the current appointment date";
        public static readonly string AppointmentDateTimeTwoWeeksLaterErrorMessage = "Appointments can only be made for 2 weeks later at most";

        public UpdateAppointmentRequestValidation()
        {
            RuleFor(x => x.PatientId).NotEmpty().WithMessage(NoPatientIdErrorMessage);
            RuleFor(x => x.AppointmentTime)
                  .GreaterThanOrEqualTo(DateTime.Now.Date.AddDays(2)).WithMessage(AppointmentDateTimeTwoDaysBeforeErrorMessage);
            RuleFor(x => x.NewAppointmentTime).GreaterThanOrEqualTo(DateTime.Now.Date.AddDays(14)).WithMessage(AppointmentDateTimeTwoWeeksLaterErrorMessage);
        }
    }
}