using CarReservationRepositories;
using CarReservationRepositories.Entities;
using CarReservationWorker;
using CarReservationWorker.Services;
using CarReservationWorkers;
using CarReservationWorkers.Constants;
using CarReservationWorkers.Models;
using CarReservationWorkers.Services;
using Moq;
using System.Net;

namespace CarReservationApiTests.Workers
{
    [TestClass]
    public class ReservationWorkerTests
    {
        private Mock<IReservationRepository> _repoMock;
        private Mock<IReservationValidationService> _validationMock;
        private Mock<IReservationAvailabilityService> _service;
        private Mock<ICarValidationService> _carService;
        private Mock<ICarRepository> _carRepository;
        private ReservationWorker _worker;

        [TestInitialize]
        public void Init()
        {
            _repoMock = new Mock<IReservationRepository>();
            _validationMock = new Mock<IReservationValidationService>();
            _service = new Mock<IReservationAvailabilityService>();
            _carService = new Mock<ICarValidationService>();
            _carRepository = new Mock<ICarRepository>();
            _worker = new ReservationWorker(_repoMock.Object, _validationMock.Object, _service.Object,
                _carService.Object, _carRepository.Object);
        }

        [TestMethod]
        public void Create_HasInvalidCarId_ReturnCarNotFoundError()
        {
            var model = new ReservationCreateModel
            {
                CarId = Guid.NewGuid(),
                DurationInMinutes = 130,
                StartTime = new DateTime(2023, 11, 1)
            };

            _carRepository.Setup(x => x.GetById(model.CarId)).Returns(null as CarEntity);

            _carService.Setup(x => x.Validate(It.IsAny<CarEntity>())).Returns(
                new ValidationResult(HttpStatusCode.NotFound, ErrorMessages.CarNotFound));

            var result = _worker.Create(model).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Error.Type, HttpStatusCode.NotFound);
            Assert.AreEqual(result.Error.Message, ErrorMessages.CarNotFound);

        }

        [TestMethod]
        public void Create_NoCarsAreAvailable_ReturnInvalidTimeSlotError()
        {
        }

        [TestMethod]
        public void Create_HasInvalidTimeDuration_ReturnInvalidTimeDurationError()
        {
        }

        [TestMethod]
        public void Create_HasNoErrors_HitsCreateMethodOnce()
        {
        }

    }
}
