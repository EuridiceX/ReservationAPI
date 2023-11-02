using CarReservationWorkers.Models;

namespace CarReservationWorkers.Services
{
    public interface IReservationService
    {
        bool IsAvailable(List<ReservationModel> reservations, ReservationModel nextReservation);
        bool IsValid(DateTime reservationTime);
    }
    public class ReservationService : IReservationService
    {
        public bool IsAvailable(List<ReservationModel> reservations, ReservationModel nextReservation)
        {
            var x = reservations.Where(x => x.CarId == nextReservation.CarId).ToList();

            bool slotIsAvailable = reservations.Any(x => (nextReservation.DateTime - x.DateTime).TotalHours <= 2);

            return slotIsAvailable;
  
        }

        public bool IsValid (DateTime reservationTime)
        {
            TimeSpan timeDifference = reservationTime - DateTime.UtcNow;

            double totalHours = timeDifference.TotalHours;

            if (totalHours < 24)
            {
                return false;
            }

            return true;
        }
    }
}
