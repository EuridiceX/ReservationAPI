using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CarReservationWorkers.Models;

namespace CarReservationAPI.ViewModels
{
    [AutoMap(typeof(ReservationModel), ReverseMap = true)]
    public class ReservationViewModel: ReservationCreateViewModel
    {
        public Guid Id { get; set; }
    }

    [AutoMap(typeof(ReservationCreateModel), ReverseMap = true)]
    public class ReservationCreateViewModel
    {
        public Guid CarId { get; set; }

        [SourceMember(nameof(ReservationCreateModel.StartTime))]
        public DateTime DateTime { get; set; }
        public double DurationInMinutes { get; set; }
    }
}
