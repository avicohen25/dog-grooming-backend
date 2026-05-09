namespace DogGrooming.Models.Request
{
    public class AddAppointmentParams
    {
        public int userId { get; set; }
        public int haircutTypeId { get; set; }
        public DateTime haircutDate { get; set; }
        public string selectedSlot { get; set; }

    }
}
