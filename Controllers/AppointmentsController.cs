using Dapper;
using DogGrooming.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Claims;

namespace DogGrooming.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly IConfiguration _configuration;

        public AppointmentsController(ILogger<AppointmentsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet("GetAppointments")]
        public async Task<AppointmentsResponse> GetAppointments()
        {
            Thread.Sleep(3000); //TODO:delete this

            string connectionString = _configuration["ConnectionStrings:DogGrooming"];
            using var conn = new SqlConnection(connectionString);

            var result = await conn.QueryAsync<Appointment>(
                "sp_getAppointments_select",
                commandType: CommandType.StoredProcedure
            );

            List<Appointment> appointments = result.ToList();

            foreach (var a in appointments)
            {
                a.DisplayHaircutDate = a.HaircutDate.ToString("dddd, dd MMMM yyyy", new CultureInfo("he-IL"));
                a.DisplayHaircutTime = a.HaircutDate.ToString("HH:mm");
            }

            var grouped = appointments
                .GroupBy(a => a.HaircutDate.Date)
                .Select(g => new GroupedAppointments
                {
                    Date = g.Key.ToString("dddd, dd MMMM yyyy", new CultureInfo("he-IL")),
                    Appointments = g.ToList()
                })
                .ToList();

            AppointmentsResponse response = new AppointmentsResponse
            {
                GroupedAppointments = grouped
            };

            return response;
        }
    }

    public class AppointmentsResponse
    {
        public List<GroupedAppointments> GroupedAppointments { get; set; }
    }


    public class GroupedAppointments
    {
        public string Date { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
