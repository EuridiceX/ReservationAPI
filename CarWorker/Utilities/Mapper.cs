using CarReservationRepositories.Entities;
using CarReservationWorkers.Models;

namespace CarReservationWorkers.Utilities
{
    public static class Mapper
    {
        public static ReservationCreateEntity MapToEntity(ReservationCreateModel createModel)
        {
            return new ReservationCreateEntity
            {
                CarId = createModel.CarId,
                StartTime = createModel.StartTime,
                EndTime = createModel.StartTime + TimeSpan.FromMinutes(createModel.DurationInMinutes)
            };
        }


        public static List<ReservationModel> MapToModel(List<ReservationEntity> entites)
        {
            return entites.Select(x => new ReservationModel
            {
                StartTime = x.StartTime,
                CarId = x.CarId,
                Id = x.Id,
                DurationInMinutes = (x.EndTime - x.StartTime).TotalMinutes

            }).ToList();
        }
    }
}
