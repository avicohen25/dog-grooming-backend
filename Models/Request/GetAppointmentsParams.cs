namespace DogGrooming.Models.Request
{
    public class GetAppointmentsParams
    {
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string userName { get; set; }

    }
}
