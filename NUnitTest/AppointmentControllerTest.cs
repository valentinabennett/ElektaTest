using Autofac.Extras.Moq;
using ElektaTest.Contracts.Requests;
using ElektaTest.Contracts.Responses;
using ElektaTest.Controllers;
using ElektaTest.Domain.Commands;
using ElektaTest.Domain.Queries;
using ElektaTest.Infrastructure;
using ElektaTest.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NUnitTest
{
    public class AppointmentControllerTest
    {
        private AutoMock _mocker;
        private AppointmentController _controller;
        private List<Appointment> expected;
        [SetUp]
        public void Setup()
        {
            _mocker = AutoMock.GetLoose();
            _controller = _mocker.Create<AppointmentController>();

            var appointments = GetAppointments();
            expected = appointments;
            _mocker.Mock<IQueryHandler>().Setup(x => x.Handle<GetAppointmentsTodayQuery, List<Appointment>>(It.IsAny<GetAppointmentsTodayQuery>()))
                .ReturnsAsync(appointments);

            _mocker.Mock<ICommandHandler>().Setup(x => x.Handle(It.IsAny<AddAppointmentCommand>()))
                .Returns(Task.CompletedTask);


            _mocker.Mock<IEmailNotificationService>().Setup(x => x.SendBookingAppointmentNotificationEmail(new NewAppointmentRequest())).Returns(Task.CompletedTask);
            _mocker.Mock<IEquipmentAvailabilityService>().Setup(x => x.CheckAvailability(It.IsAny<DateTime>())).ReturnsAsync(new EquipmentAvailabilityResponse { EequipmentId = 1, IsAvailable = true, Date = DateTime.Now });

        }

        [Test]
        public async Task Should_return_Ok_getting_appointments()
        {
            var response = await _controller.GetAppointmentsTodayAsync();

            Assert.IsNotNull(response);
            var result = response.Result as OkObjectResult;
            Assert.IsNotNull(result);
            var appointments = result.Value as List<AppointmentResponse>;
            Assert.AreEqual(expected[0].PatientId, appointments[0].PatientId);
            Assert.AreEqual(expected[0].AppointmentTime, appointments[0].AppointmentTime);

        }

        [Test]
        public async Task Should_return_BadRequest_for_add_appointment_if_model_invalid()
        {
            var request = new NewAppointmentRequest { PatientId = Guid.Empty, AppointmentTime = DateTime.Now };

            var response = await _controller.BookNewAppointmentAsync(request);

            Assert.IsNotNull(response);
            var result = response as BadRequestObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task Should_return_Ok_for_add_appointment()
        {
            var date = DateTime.Now.AddDays(15);
            var appDate = new DateTime(date.Year, date.Month, date.Day, 10, 0, 0);
            var request = new NewAppointmentRequest { PatientId = Guid.NewGuid(), AppointmentTime = appDate };

            var response = await _controller.BookNewAppointmentAsync(request);

            Assert.IsNotNull(response);
            var result = response as OkResult;
            Assert.IsNotNull(result);

            _mocker.Mock<IEquipmentAvailabilityService>().Verify(x => x.CheckAvailability(It.IsAny<DateTime>()), Times.Once);
            _mocker.Mock<ICommandHandler>().Verify(x => x.Handle(It.IsAny<AddAppointmentCommand>()), Times.Once);
            _mocker.Mock<IEmailNotificationService>().Verify(x => x.SendBookingAppointmentNotificationEmail(It.IsAny<NewAppointmentRequest>()), Times.Once);
        }

        private List<Appointment> GetAppointments()
        {
            return new List<Appointment>
            {
                new Appointment{
                    PatientId=Guid.NewGuid(),
                    AppointmentTime = DateTime.Now.AddDays(15)
                },
                new Appointment
                {
                    PatientId = Guid.NewGuid(),
                    AppointmentTime = DateTime.Now.AddDays(14)
                }
            };
        }
    }
}
