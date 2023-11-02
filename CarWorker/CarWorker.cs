using AutoMapper;
using CarReservationRepositories;
using CarReservationWorkers.Models;

namespace CarWorker
{
    public interface ICarWorker
    {
        void Create(CarCreateModel car);
        void Update(CarCreateModel car, Guid id);
        CarModel GetById(Guid id);
        void Remove(Guid id);

    }

    public class CarWorker : ICarWorker
    {
        private readonly ICarRepository _repository;
        private readonly IMapper _mapper;

        public CarWorker(ICarRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(CarCreateModel car)
        {
            var entity = _mapper.Map<CarCreateEntity>(car);
            _repository.Create(entity);
        }

        public CarModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(CarCreateModel car, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}