using AutoMapper;

namespace CarReservationRepositories.Entities
{
    public class ReservationEntity : ReservationCreateEntity
    {
        public Guid Id { get; set; }

    }

    [AutoMap(typeof(ReservationEntity), ReverseMap = true)]
    public class ReservationCreateEntity
    {
        public Guid CarId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
