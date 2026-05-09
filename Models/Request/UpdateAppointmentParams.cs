namespace DogGrooming.Models.Request
{
    public class UpdateAppointmentParams
    {
        public int appointmentId { get; set; }
        public int userId { get; set; }
        public int haircutTypeId { get; set; }
        public DateTime haircutDate { get; set; }
        public string selectedSlot { get; set; }

    }
}
