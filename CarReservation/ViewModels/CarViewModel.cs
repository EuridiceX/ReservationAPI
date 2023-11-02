using AutoMapper;
using CarReservationWorkers.Models;

namespace CarReservation.ViewModels
{
    [AutoMap(typeof(CarModel), ReverseMap = true)]
    public class CarViewModel : CarCreateViewModel
    {
        public Guid Id { get; set; }   
    }

    [AutoMap(typeof(CarCreateModel), ReverseMap = true)]
    public class CarCreateViewModel
    {
        public string Model { get; set; }
        public string Number { get; set; }
    }
   
}
