using CarReservationRepositories;
using CarReservationRepositories.Entities;
using CarReservationWorker.Services;
using CarReservationWorkers;
using CarReservationWorkers.Models;
using CarReservationWorkers.Services;

namespace CarReservationWorker
{
    public interface IReservationWorker
    {
        Task<List<ReservationModel>> GetAll();
        Task<ValidationResult> Create(ReservationCreateModel createModel);
    }
    public class ReservationWorker : IReservationWorker
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationValidationService _validationService;
        private readonly IReservationAvailabilityService _reservationService;
        private readonly ICarValidationService _carValidationService;
        private readonly ICarRepository _carRepository;

        public ReservationWorker(IReservationRepository repository,
            IReservationValidationService validationService,
            IReservationAvailabilityService reservationService,
            ICarValidationService carValidationService,
            ICarRepository carRepository)
        {
            _reservationRepository = repository;
            _validationService = validationService;
            _reservationService = reservationService;
            _carValidationService = carValidationService;
            _carRepository = carRepository;
        }

        public Task<ValidationResult> Create(ReservationCreateModel createModel)
        {
            var validationResults = ValidateModel(createModel);

            if (validationResults.Any(x => x.HasError))
            {
                return Task.FromResult(validationResults.First(x => x.HasError));
            }

            var entity = MapToEntity(createModel);

            _reservationRepository.Create(entity);

            return Task.FromResult(new ValidationResult());
        }

        private List<ValidationResult> ValidateModel(ReservationCreateModel model)
        {
            var validationResults = new List<ValidationResult>();

            var carEntity = _carRepository.GetById(model.CarId);

            var carValidationResult = _carValidationService.Validate(carEntity);

            validationResults.Add(carValidationResult);

            ValidationResult validationResult = _validationService.IsValidModel(model);

            validationResults.Add(validationResult);

            var reservations = _reservationRepository.GetAll();

            var entity = MapToEntity(model);

            ValidationResult availabilityResult = _reservationService.IsAvailableTimeSlot(reservations, entity);

            validationResults.Add(availabilityResult);

            return validationResults;
        }

        private ReservationCreateEntity MapToEntity(ReservationCreateModel createModel)
        {
            return new ReservationCreateEntity
            {
                CarId = createModel.CarId,
                StartTime = createModel.StartTime,
                EndTime = createModel.StartTime + TimeSpan.FromMinutes(createModel.DurationInMinutes)
            };
        }

        public Task<List<ReservationModel>> GetAll()
        {
            var entites = _reservationRepository.GetAll();

            var models = MapToModel(entites);

            return Task.FromResult(models);
        }

        private List<ReservationModel> MapToModel(List<ReservationEntity> entites)
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
