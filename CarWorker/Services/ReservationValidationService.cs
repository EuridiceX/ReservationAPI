using CarReservationWorkers.Constants;
using CarReservationWorkers.Models;

namespace CarReservationWorkers.Services
{
    public interface IReservationValidationService
    {
        ValidationResult IsValidModel(ReservationCreateModel reservation);
    }
    public class ReservationValidationService : IReservationValidationService
    {
        public ValidationResult IsValidModel(ReservationCreateModel reservation)
        {
            var validationResult = new ValidationResult();

            TimeSpan timeDifference = DateTime.UtcNow - reservation.StartTime;

            double totalHours = timeDifference.TotalHours;

            if (totalHours < 24)
            {
                validationResult.CreateError(System.Net.HttpStatusCode.BadRequest, ErrorMessages.InvalidTimeReservation);
            }

            if (reservation.DurationInMinutes > 120 || reservation.DurationInMinutes < 0)
            {
                validationResult.CreateError(System.Net.HttpStatusCode.BadRequest, ErrorMessages.InvalidDuration);
            }

            return validationResult;
        }
    }
}
