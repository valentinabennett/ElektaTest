using ElektaTest.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektaTest.Domain.Queries
{
    public class GetAppointmentsTodayQuery : IQuery
    {
    }

    public class GetAppointmentsTodayQueryHandler : IQueryHandler<GetAppointmentsTodayQuery, List<Appointment>>
    {
        private readonly AppointmentContext _context;

        public GetAppointmentsTodayQueryHandler(AppointmentContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> Handle(GetAppointmentsTodayQuery query)
        {
            var today = DateTime.Today;
            var tomorrow = DateTime.Today.AddDays(1);

            var todayQuery = await _context.Appointments
                .AsNoTracking()
                .Where(x => x.AppointmentTime >= today && x.AppointmentTime < tomorrow).ToListAsync();

            return todayQuery;
        }
    }
}
