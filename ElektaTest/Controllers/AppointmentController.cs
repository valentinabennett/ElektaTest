using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using System.Net;
using ElektaTest.Contracts.Requests;
using ElektaTest.Validations;
using ElektaTest.Services;

namespace ElektaTest.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly IEquipmentAvailabilityService _equipmentAvailabilityService;
        private readonly IEmailNotificationService _emailNotificationService;
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(ILogger<AppointmentController> logger, 
            IEmailNotificationService emailNotificationService, IEquipmentAvailabilityService equipmentAvailabilityService, IAppointmentService appointmentService)
        {
            _logger = logger;
            _emailNotificationService = emailNotificationService;
            _equipmentAvailabilityService = equipmentAvailabilityService;
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// Request to book an appointment
        /// </summary>
        /// <param name="request">Details of a appointment</param>
        /// <returns>Details of the new appointment</returns>
        [HttpPost]
        [OpenApiOperation("BookNewAppointment")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> BookNewAppointmentAsync(NewAppointmentRequest request)
        {
            _logger.LogDebug("BookNewAppointment");

            var validation = new NewAppointmentRequestValidation().Validate(request);
            if (!validation.IsValid)
            {
                foreach (var failure in validation.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _equipmentAvailabilityService.CheckAvailability(request.AppointmentTime);
                if (response.IsAvailable)
                {
                    await _appointmentService.AddAppointment(request, response.EequipmentId);
                    await _emailNotificationService.SendBookingAppointmentNotificationEmail(request);
                }
                else
                {
                    await _emailNotificationService.SendNotAvailableNotificationEmail(request);
                }
            }
            catch (Exception err)
            {
                _logger.LogWarning($"The appointment time is nt available for {request.PatientId} time: {request.AppointmentTime}", err.Message);
                return NotFound();
            }
            return Ok();
        }
    }
}
