using ElektaTest.Contracts.Mappings;
using ElektaTest.Contracts.Requests;
using ElektaTest.Contracts.Responses;
using ElektaTest.Domain.Commands;
using ElektaTest.Domain.Queries;
using ElektaTest.Exceptions;
using ElektaTest.Infrastructure;
using ElektaTest.Services;
using ElektaTest.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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
        private readonly IQueryHandler _queryHandler;
        private readonly ICommandHandler _commandHandler;

        public AppointmentController(ILogger<AppointmentController> logger,
            IEmailNotificationService emailNotificationService, IEquipmentAvailabilityService equipmentAvailabilityService,
            IQueryHandler queryHandler, ICommandHandler commandHandler)
        {
            _logger = logger;
            _emailNotificationService = emailNotificationService;
            _equipmentAvailabilityService = equipmentAvailabilityService;
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
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
                    var command = new AddAppointmentCommand(request.PatientId, request.AppointmentTime, response.EequipmentId);
                    await _commandHandler.Handle(command);
                    await _emailNotificationService.SendBookingAppointmentNotificationEmail(request);
                }
                else
                {
                    _logger.LogWarning($"The appointment time is not available for {request.PatientId} time: {request.AppointmentTime}");
                    await _emailNotificationService.SendNotAvailableNotificationEmail(request);
                }
            }
            catch (Exception err)
            {
                _logger.LogError($"Error. The appointment cannot be made for {request.PatientId} time: {request.AppointmentTime}", err.Message);
                return NotFound();
            }
            return Ok();
        }

        /// <summary>
        /// Cancel an appointment, set its to cancelled
        /// </summary>
        /// <param name="request">Details of appointment</param>
        /// <returns>No Content status</returns>
        [HttpPut("cancel")]
        [OpenApiOperation("CancelAppointment")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CancelAppointmentAsync(CancelAppointmentRequest request)
        {
            _logger.LogDebug("CancelAppointment");

            var validation = new CancelAppointmentRequestValidation().Validate(request);
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
                var command = new CancelAppointmentCommand(request.PatientId, request.AppointmentTime);

                await _commandHandler.Handle(command); await _equipmentAvailabilityService.CancelAppointment(request);

                return NoContent();
            }
            catch (AppointmentNotFoundException ex)
            {
                _logger.LogError(ex, "Unable to find appointment {patientId}", request.PatientId);

                return NotFound();
            }
        }

        /// <summary>
        /// Update an appointment
        /// </summary>
        /// <param name="request">Details of appointment</param>
        /// <returns>No Content status</returns>
        [HttpPut]
        [OpenApiOperation("UpdateAppointment")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAppointmentAsync(UpdateAppointmentRequest request)
        {
            _logger.LogDebug("UpdateAppointment");

            var validation = new UpdateAppointmentRequestValidation().Validate(request);
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
                var response = await _equipmentAvailabilityService.CheckAvailability(request.NewAppointmentTime);
                if (response.IsAvailable)
                {
                    var command = new UpdateAppointmentCommand(request.PatientId, request.AppointmentTime, request.NewAppointmentTime);
                    await _commandHandler.Handle(command);
                    await _equipmentAvailabilityService.UpdateAppointment(request);
                    return NoContent();

                }
                else
                {
                    _logger.LogError($"The equipment for this appointment date {request.NewAppointmentTime} is not available for {request.PatientId} ", request.PatientId);
                    return NotFound();
                }

            }
            catch (AppointmentNotFoundException ex)
            {
                _logger.LogError(ex, "Unable to find appointment {patientId}", request.PatientId);

                return NotFound();
            }
        }

        /// <summary>
        /// Get today's appointments
        /// </summary>
        /// <returns>Appointments for today</returns>
        [HttpGet("today")]
        [OpenApiOperation("GetAppointmentsToday")]
        [ProducesResponseType(typeof(List<AppointmentResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<AppointmentResponse>>> GetAppointmentsTodayAsync()
        {
            _logger.LogDebug("GetAppointmentsToday");

            try
            {
                var appointmentsToday =
                    await _queryHandler.Handle<GetAppointmentsTodayQuery, List<Appointment>>(new GetAppointmentsTodayQuery());
                var response = appointmentsToday.Select(AppointmentToAppointmentResponseMapping.Map).ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to get the appointments for today", ex.Message);
                throw;
            }
        }
    }
}
