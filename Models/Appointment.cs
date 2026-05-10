namespace DogGrooming.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int HaircutTypeId { get; set; }
        public DateTime HaircutDate { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DisplayHaircutDate { get; set; }
        public string DisplayHaircutTime { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HaircutName { get; set; }
        public int DurationMinutes { get; set; }
        public float Price { get; set; }

        public int TotalAppointments { get; set; }
    }
}