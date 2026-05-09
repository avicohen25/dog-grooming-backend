namespace DogGrooming.Models.Response
{
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
