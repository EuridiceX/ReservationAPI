using CarReservationRepositories;
using CarReservationWorker.Services;
using CarReservationWorkers.Models;
using CarReservationWorkers.Services;
using CarReservationWorkers.Utilities;

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
            var validationResult = ValidateModel(createModel);

            if (validationResult.HasError)
            {
                return Task.FromResult(validationResult);
            }

            var entity = Mapper.MapToEntity(createModel);

            _reservationRepository.Create(entity);

            return Task.FromResult(new ValidationResult());
        }

        public Task<List<ReservationModel>> GetAll()
        {
            var entites = _reservationRepository.GetAll();

            var models = Mapper.MapToModel(entites);

            return Task.FromResult(models);
        }

        private ValidationResult ValidateModel(ReservationCreateModel model)
        {
            var carEntity = _carRepository.GetById(model.CarId);

            var carValidationResult = _carValidationService.Validate(carEntity);

            if(carValidationResult.HasError)
            {
                return carValidationResult;
            }

            ValidationResult validationResult = _validationService.IsValidModel(model);

            if (validationResult.HasError)
            {
                return validationResult;
            }

            var reservations = _reservationRepository.GetAll();

            var entity = Mapper.MapToEntity(model);

            ValidationResult availabilityResult = _reservationService.IsAvailableTimeSlot(reservations, entity);

            return availabilityResult;
        }
       
    }
}
