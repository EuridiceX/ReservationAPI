using CarReservationWorkers;
using System.Text.RegularExpressions;

namespace CarReservationWorker.Services
{
    public interface ICarValidationService
    {
        OperationResult ValidateNumber(string number);
    }

    public class CarValidationService : ICarValidationService
    {
        public OperationResult ValidateNumber(string number)
        {
            var result = new OperationResult();
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
