namespace DogGrooming.Models.Response
{
    public class GetAppointmentDataResponse
    {
        public DateTime SelectedDate { get; set; }
        public List<HaircutType> HaircutTypes { get; set; }
        public int SelectedHaircutType { get; set; }
        public List<string> AvailableSlots { get; set; }
        public string SelectedSlot { get; set; }
    }

    public class HaircutType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DurationMinutes { get; set; }
        public float Price { get; set; }
    }
}
