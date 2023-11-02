using AutoMapper;

namespace CarReservationRepositories.Entities
{
    public class CarEntity : CarCreateEntity
    {
        public Guid Id { get; set; }

    }

    [AutoMap(typeof(CarEntity), ReverseMap = true)]
    public class CarCreateEntity
    {
        public string Model { get; set; }
        public string Number { get; set; }
    }
}
