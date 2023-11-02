namespace CarReservationWorkers.Models
{
    public class ReservationModel
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; } 
        public DateTime  DateTime { get; set; }
    }
}
