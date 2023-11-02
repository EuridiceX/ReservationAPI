using AutoMapper;
using CarReservationRepositories.Entities;

namespace CarReservationWorkers.Models
{
    [AutoMap(typeof(CarEntity),ReverseMap = true)]
    public class CarModel : CarCreateModel 
    {
        public Guid Id { get; set; }
    }

    [AutoMap(typeof(CarCreateEntity), ReverseMap = true)]
    public class CarCreateModel
    {
        public string Model { get; set; }
        public string Number { get; set; }
    }
}
