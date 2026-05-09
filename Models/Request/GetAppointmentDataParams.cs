namespace DogGrooming.Models.Request
{
    public class GetAppointmentDataParams
    {
        public int AppointmentId { get; set; }
        public int HaircutTypeId { get; set; }
        public DateTime HaircutDate { get; set; }
    }
}
