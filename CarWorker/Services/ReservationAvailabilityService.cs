using CarReservationRepositories.Entities;
using CarReservationWorkers.Constants;
using CarReservationWorkers.Utilities;

namespace CarReservationWorkers.Services
{
    public interface IReservationAvailabilityService
    {
        ValidationResult IsAvailableTimeSlot(List<ReservationEntity> reservations, ReservationCreateEntity nextReservation);

    }
    public class ReservationAvailabilityService : IReservationAvailabilityService
    {
        public ValidationResult IsAvailableTimeSlot(List<ReservationEntity> reservations, ReservationCreateEntity nextReservation)
        {
            var validationResult = new ValidationResult();

            var x = reservations.Where(x => x.CarId == nextReservation.CarId).ToList();

            if (!x.Any())
            {
                return validationResult;
            }

            bool slotIsAvailable = reservations.All(x => IsSlotAvailable(x, nextReservation));

            if (!slotIsAvailable)
            {
                validationResult.CreateError(System.Net.HttpStatusCode.BadRequest, ErrorMessages.InvalidTimeSlot);
            }
            return validationResult;
        }

        private bool IsSlotAvailable(ReservationEntity x, ReservationCreateEntity nextReservation)
        {
            return nextReservation.StartTime < x.StartTime && nextReservation.EndTime < x.StartTime
                  || nextReservation.StartTime > x.EndTime && nextReservation.EndTime > x.EndTime;
        }

    }
}
