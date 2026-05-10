using DogGrooming.Managers.Contracts;
using DogGrooming.Models;
using DogGrooming.Models.Request;
using DogGrooming.Models.Response;
using DogGrooming.Providers.Contracts;
using System.Globalization;

namespace DogGrooming.Managers.Managers
{
    public class AppointmentsManager: IAppointmentsManager
    {
        private readonly IConfiguration _configuration;
        private readonly IGetAppointmentsProvider _getAppointmentsProvider;
        private readonly IAddAppointmentProvider _addAppointmentProvider;
        private readonly IUpdateAppointmentProvider _updateAppointmentProvider;
        private readonly IDeleteAppointmentProvider _deleteAppointmentProvider;
        private readonly IGetHaircutTypesProvider _getHaircutTypesProvider;

        public AppointmentsManager(IConfiguration configuration, IGetAppointmentsProvider getAppointmentsProvider, IAddAppointmentProvider addAppointmentProvider, IUpdateAppointmentProvider updateAppointmentProvider, IDeleteAppointmentProvider deleteAppointmentProvider, IGetHaircutTypesProvider getHaircutTypesProvider)
        {
            _configuration = configuration;
            _getAppointmentsProvider = getAppointmentsProvider;
            _addAppointmentProvider = addAppointmentProvider;
            _updateAppointmentProvider = updateAppointmentProvider;
            _deleteAppointmentProvider = deleteAppointmentProvider;
            _getHaircutTypesProvider = getHaircutTypesProvider;
        }


        public async Task<AppointmentsResponse> GetAppointments(GetAppointmentsParams getAppointmentsParams)
        {
            List<Appointment> appointments = await _getAppointmentsProvider.GetData(getAppointmentsParams);

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


        public async Task<GetAppointmentDataResponse> GetAppointmentData(GetAppointmentDataParams getAppointmentDataParams)
        {
            List<Appointment> appointments = await _getAppointmentsProvider.GetData(new GetAppointmentsParams());
            List<HaircutType> haircutTypes = await _getHaircutTypesProvider.GetData();

            Appointment appointment = appointments.FirstOrDefault(x => x.Id == getAppointmentDataParams.AppointmentId);

            DateTime selectedDate = appointment != null ? appointment.HaircutDate : DateTime.Now;
            if (getAppointmentDataParams.HaircutDate != DateTime.MinValue)
            {
                selectedDate = getAppointmentDataParams.HaircutDate;
            }

            HaircutType haircutType = haircutTypes.FirstOrDefault();
            int haircutTypeId = appointment != null ? appointment.HaircutTypeId : haircutType.Id;

            if (getAppointmentDataParams.HaircutTypeId > 0)
            {
                haircutTypeId = getAppointmentDataParams.HaircutTypeId;
                haircutType = haircutTypes.FirstOrDefault(x => x.Id == haircutTypeId);
            }

            List<string> availableSlots = new List<string>();

            var date = selectedDate;
            var dailyAppointments = appointments
                .Where(a => a.HaircutDate.Date == date.Date && !a.IsCanceled)
                .ToList();

            var slots = GenerateDailySlots(date);

            foreach (var slot in slots)
            {
                var start = date.Date + slot;

                if (start <= DateTime.Now) { continue; }
                    
                int selectedDuration = haircutType != null ? haircutType.DurationMinutes : 0;
                var end = start.AddMinutes(selectedDuration);

                bool overlaps = dailyAppointments.Any(a =>
                {
                    var existingDuration = GetDurationByType(a.HaircutTypeId);
                    var existingStart = a.HaircutDate;
                    var existingEnd = existingStart.AddMinutes(existingDuration);

                    return IsOverlapping(start, end, existingStart, existingEnd);
                });

                if (!overlaps)
                {
                    availableSlots.Add(slot.ToString(@"hh\:mm"));
                }
            }

            string selectedSlot = appointment != null ? selectedDate.ToString("HH:mm") : availableSlots.FirstOrDefault();

            GetAppointmentDataResponse response = new GetAppointmentDataResponse
            {
                SelectedDate = selectedDate,
                HaircutTypes = haircutTypes,
                SelectedHaircutType = haircutTypeId,
                AvailableSlots = availableSlots,
                SelectedSlot = selectedSlot
            };

            return response;
        }


        public async Task<AddAppointmentResponse> AddAppointment(AddAppointmentParams addAppointmentParams)
        {
            bool isSuccess = await _addAppointmentProvider.GetData(addAppointmentParams);

            AddAppointmentResponse response = new AddAppointmentResponse
            {
                isSuccess = isSuccess
            };

            return response;
        }


        public async Task<AddAppointmentResponse> UpdateAppointment(UpdateAppointmentParams updateAppointmentParams)
        {
            bool isSuccess = await _updateAppointmentProvider.GetData(updateAppointmentParams);

            AddAppointmentResponse response = new AddAppointmentResponse
            {
                isSuccess = isSuccess
            };

            return response;
        }


        public async Task<DeleteAppointmentResponse> DeleteAppointment(DeleteAppointmentParams deleteAppointmentParams)
        {
            bool isSuccess = await _deleteAppointmentProvider.GetData(deleteAppointmentParams);

            DeleteAppointmentResponse response = new DeleteAppointmentResponse
            {
                isSuccess = isSuccess
            };

            return response;
        }




        //------------------------------------------------------
        private int GetDurationByType(int haircutTypeId)
        {
            return haircutTypeId switch
            {
                1 => 30,
                2 => 45,
                3 => 60,
                _ => 30
            };
        }

        private List<TimeSpan> GenerateDailySlots(DateTime date)
        {
            var slots = new List<TimeSpan>();
            var start = new TimeSpan(9, 0, 0);
            var end = new TimeSpan(18, 0, 0);

            while (start < end)
            {
                slots.Add(start);
                start = start.Add(TimeSpan.FromMinutes(15));
            }

            return slots;
        }

        private bool IsOverlapping(DateTime newStart, DateTime newEnd, DateTime existingStart, DateTime existingEnd)
        {
            return existingStart < newEnd && existingEnd > newStart;
        }
        //------------------------------------------------------



    }
}
