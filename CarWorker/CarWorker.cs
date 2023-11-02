using AutoMapper;
using CarReservationRepositories;
using CarReservationRepositories.Entities;
using CarReservationWorker.Services;
using CarReservationWorkers;
using CarReservationWorkers.Models;

namespace CarReservationWorker
{
    public interface ICarWorker
    {
        Task<OperationResult> Create(CarCreateModel car);
        Task<OperationResult> Update(CarCreateModel car, Guid id);
        Task<CarModel> GetById(Guid id);
        Task<List<CarModel>> GetAll();
        Task Remove(Guid id);
    }

    public class CarWorker : ICarWorker
    {
        private readonly ICarRepository _repository;
        private readonly ICarValidationService _validationService;
        private readonly IMapper _mapper;

        public CarWorker(ICarRepository repository, IMapper mapper,
            ICarValidationService validationService)
        {
            _repository = repository;
            _mapper = mapper;
            _validationService = validationService;
        }

        public Task<OperationResult> Create(CarCreateModel car)
        {
            OperationResult result = _validationService.ValidateNumber(car.Number);
          
            if (result.HasError)
            {
                return Task.FromResult(result);
            }
            var entity = _mapper.Map<CarCreateEntity>(car);
          
            _repository.Create(entity);

            return Task.FromResult(result);
        }

        public Task<List<CarModel>> GetAll()
        {
            var entites = _repository.GetAll();

            var models = _mapper.Map<List<CarModel>>(entites);

            return Task.FromResult(models);
        }

        public Task<CarModel> GetById(Guid id)
        {
           var entity = _repository.GetById(id);

            return Task.FromResult(_mapper.Map<CarModel>(entity));
        }

        public Task Remove(Guid id)
        {
            _repository.Remove(id);

            return Task.CompletedTask;
        }

        public Task<OperationResult> Update(CarCreateModel car, Guid id)
        {
            OperationResult result = _validationService.ValidateNumber(car.Number);
            if (result.HasError)
            {
                return Task.FromResult(result);
            }

            var entity = _mapper.Map<CarCreateEntity>(car);
          
            _repository.Update(entity, id);

            return Task.FromResult(result);
        }
    }
}