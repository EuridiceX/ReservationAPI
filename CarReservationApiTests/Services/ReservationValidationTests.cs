using CarReservationWorkers.Constants;
using CarReservationWorkers.Models;
using CarReservationWorkers.Services;
using System.Net;

namespace CarReservationApiTests.Services
{
    [TestClass]
    public class ReservationValidationTests
    {
        private ReservationValidationService _service;

        [TestInitialize]
        public void Init()
        {
            _service = new ReservationValidationService();
        }

        [TestMethod]
        public void IsValidModel_HasDurationTimeGreaterThan120Minutes_ShouldReturnInvalidDurationError()
        {
            var model = new ReservationCreateModel
            {
                DurationInMinutes = 130,
                StartTime = new DateTime(2023, 11, 1)
            };

            var result = _service.IsValidModel(model);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Error.Type, HttpStatusCode.BadRequest);
            Assert.AreEqual(result.Error.Message, ErrorMessages.InvalidDuration);

        }

        [TestMethod]
        public void IsValidModel_HasDurationTimeLessThanZero_ShouldReturnInvalidDurationError()
        {
            var model = new ReservationCreateModel
            {
                DurationInMinutes = -3,
                StartTime = new DateTime(2023, 11, 1)
            };

            var result = _service.IsValidModel(model);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Error.Type, HttpStatusCode.BadRequest);
            Assert.AreEqual(result.Error.Message, ErrorMessages.InvalidDuration);

        }

        [TestMethod]
        public void IsValidModel_HasDurationUpTo2Hours_ShouldNoReturnError()
        {
            var model = new ReservationCreateModel
            {
                DurationInMinutes = 40,
                StartTime = new DateTime(2023, 11, 1)
            };

            var result = _service.IsValidModel(model);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.HasError);

        }
    }
}