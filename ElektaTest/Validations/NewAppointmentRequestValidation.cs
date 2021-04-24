using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElektaTest.Contracts.Requests;
using FluentValidation;

namespace ElektaTest.Validations
{
    public class NewAppointmentRequestValidation : AbstractValidator<NewAppointmentRequest>
    {
        public static readonly string NoPatientIdErrorMessage = "PatientId is required";
        public static readonly string AppointmentDateTimeTwoWeeksBeforeErrorMessage = "AppointmentDateTime should be 2 weeks before";
        public static readonly string AppointmentDateTimeWorkingHoursErrorMessage = "AppointmentTime is not between 8:00 and 16:00";

        public NewAppointmentRequestValidation()
        {
          RuleFor(x => x.PatientId).NotEmpty().WithMessage(NoPatientIdErrorMessage);
          RuleFor(x => x.AppointmentTime)
                .GreaterThanOrEqualTo(DateTime.Now.Date.AddDays(14)).WithMessage(AppointmentDateTimeTwoWeeksBeforeErrorMessage);
            RuleFor(x => x.AppointmentTime).Custom((request, context) =>
            {
                var start = StartTime(request);
                var end = EndTime(request);
                if (request < start || request.AddHours(1) > end)
                {
                    context.AddFailure(AppointmentDateTimeWorkingHoursErrorMessage);
                }
            });
        }

        private DateTime StartTime(DateTime appointmentTime)
        {
            return new DateTime(appointmentTime.Year, appointmentTime.Month, appointmentTime.Day, 8, 0, 0);
        }
        private DateTime EndTime(DateTime appointmentTime)
        {
            return new DateTime(appointmentTime.Year, appointmentTime.Month, appointmentTime.Day, 16, 0, 0);
        }
    }
}


     
