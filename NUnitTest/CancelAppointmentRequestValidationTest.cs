using NUnit.Framework;
using ElektaTest.Validations;
using System.Threading.Tasks;
using ElektaTest.Contracts.Requests;
using System;
using System.Linq;

namespace NUnitTest
{
    public class CancelAppointmentRequestValidationTest
    {
        private CancelAppointmentRequestValidation _validator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _validator = new CancelAppointmentRequestValidation();
        }

        [Test]
        public async Task Should_pass_validation()
        {
            var appTime = DateTime.Now.AddDays(4);
            var time = new DateTime(appTime.Year, appTime.Month, appTime.Day, 10, 0, 0);
            var request = new CancelAppointmentRequest { PatientId = Guid.NewGuid(), AppointmentTime = time };

            var result = await _validator.ValidateAsync(request);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public async Task Should_return_appointment_should_be_up_3_days_before_error()
        {
            var appTime = DateTime.Now.AddDays(2);
            var time = new DateTime(appTime.Year, appTime.Month, appTime.Day, 10, 0, 0);
            var request = new CancelAppointmentRequest { PatientId = Guid.NewGuid(), AppointmentTime = time };

            var result = await _validator.ValidateAsync(request);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage == CancelAppointmentRequestValidation.AppointmentDateTimeThreeDaysBeforeErrorMessage));
        }

        [Test]
        public async Task Should_return_no_patient_id_error()
        {
            var appTime = DateTime.Now.AddDays(6);
            var time = new DateTime(appTime.Year, appTime.Month, appTime.Day, 10, 0, 0);
            var request = new CancelAppointmentRequest { PatientId = Guid.Empty, AppointmentTime = time };

            var result = await _validator.ValidateAsync(request);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage == CancelAppointmentRequestValidation.NoPatientIdErrorMessage));
        }

    }
}
