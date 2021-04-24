using NUnit.Framework;
using ElektaTest.Validations;
using System.Threading.Tasks;
using ElektaTest.Contracts.Requests;
using System;
using System.Linq;

namespace NUnitTest
{
    public class NewAppointmentRequestValidationTests
    {

        private NewAppointmentRequestValidation _validator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _validator = new NewAppointmentRequestValidation();
        }

        [Test]
        public async Task Should_pass_validation()
        {
            var appTime = DateTime.Now.AddDays(15);
            var time = new DateTime(appTime.Year, appTime.Month, appTime.Day, 10, 0, 0);
            var request = new NewAppointmentRequest { PatientId = Guid.NewGuid(), AppointmentTime = time };

            var result = await _validator.ValidateAsync(request);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public async Task Should_return_missing_pation_id_error()
        {
            var appTime = DateTime.Now.AddDays(15);
            var time = new DateTime(appTime.Year, appTime.Month, appTime.Day, 10, 0, 0);
            var request = new NewAppointmentRequest { PatientId = Guid.Empty, AppointmentTime = time };


            var result = await _validator.ValidateAsync(request);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage == NewAppointmentRequestValidation.NoPatientIdErrorMessage));
        }

        [Test]
        public async Task Should_return_appointment_two_weeks_before_error()
        {
            var appTime = DateTime.Now.AddDays(10);
            var time = new DateTime(appTime.Year, appTime.Month, appTime.Day, 10, 0, 0);
            var request = new NewAppointmentRequest { PatientId = Guid.NewGuid(), AppointmentTime = time };


            var result = await _validator.ValidateAsync(request);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage == NewAppointmentRequestValidation.AppointmentDateTimeTwoWeeksBeforeErrorMessage));
        }

        [Test]
        public async Task Should_return_appointment_working_hours_error()
        {
            var appTime = DateTime.Now.AddDays(16);
            var time = new DateTime(appTime.Year, appTime.Month, appTime.Day, 15, 30, 0);
            var request = new NewAppointmentRequest { PatientId = Guid.NewGuid(), AppointmentTime = time };
         

            var result = await _validator.ValidateAsync(request);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage == NewAppointmentRequestValidation.AppointmentDateTimeWorkingHoursErrorMessage));
        }
    }
}