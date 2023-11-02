namespace CarReservationWorkers.Constants
{
    public class ErrorMessages
    {
        public const string InvalidNumber = "The number provided is not valid. Please provide a number that will match the pattern C<number>.";
        public const string CarNotFound = "Car with specified id not exists.";
        public const string InvalidTimeSlot = "Cannot create the reseration because the specified car is not available for the time and duration provided.";
        public const string InvalidTimeReservation = "Reservation should be made up to 24 hours in advance. Please consider other time slot.";
        public const string InvalidDuration = "Duration of a reservation can have maximum 120 minutes and should be greater than zero.";
    }
}
