using AutoMapper;
using CarReservationRepositories.Entities;

namespace CarReservationWorkers.Models
{
    public class ReservationModel : ReservationCreateModel
    {
        public Guid Id { get; set; }
      
    }

    public class ReservationCreateModel
    {
        public Guid CarId { get; set; }
        public DateTime StartTime { get; set; }
        public double DurationInMinutes { get; set; }

    }

}
