namespace DogGrooming.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int HaircutTypeId { get; set; }
        public DateTime HaircutDate { get; set; }
        //public DateTime CreatedDate { get; set; }
        public string DisplayHaircutDate { get; set; }
        public string DisplayHaircutTime { get; set; }
    }
}