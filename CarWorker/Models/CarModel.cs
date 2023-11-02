using AutoMapper;
using CarReservationRepositories.Entities;

namespace CarReservationWorkers.Models
{
    [AutoMap(typeof(CarEntity),ReverseMap = true)]
    public class CarModel
    {
        public Guid Id { get; set; }
    }
    public class CarCreateModel
    {
        public string Model { get; set; }
        public string Number { get; set; }
    }
}
