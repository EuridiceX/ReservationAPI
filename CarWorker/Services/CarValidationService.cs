using CarReservationRepositories.Entities;
using CarReservationWorkers.Constants;
using CarReservationWorkers.Utilities;
using System.Text.RegularExpressions;

namespace CarReservationWorker.Services
{
    public interface ICarValidationService
    {
        ValidationResult ValidateNumber(string number);
        ValidationResult Validate(CarEntity entity);
    }

    public class CarValidationService : ICarValidationService
    {
        public ValidationResult Validate(CarEntity entity)
        {
            var result = new ValidationResult();
            if (entity == null)
            {
                result.CreateError(System.Net.HttpStatusCode.NotFound, ErrorMessages.CarNotFound);
            }
            return result;
        }

        public ValidationResult ValidateNumber(string number)
        {
            var result = new ValidationResult();
            string pattern = @"^C<\d+>$";

            if (Regex.IsMatch(number, pattern))
            {
                return result;
            }

            result.CreateError(System.Net.HttpStatusCode.BadRequest, ErrorMessages.InvalidNumber);
            return result;
        }


    }
}
