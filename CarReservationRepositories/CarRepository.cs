using AutoMapper;
using CarReservationRepositories.Entities;

namespace CarReservationRepositories
{
    public interface ICarRepository
    {
        void Create(CarCreateEntity car);
        void Update(CarCreateEntity car, Guid id);
        CarEntity GetById(Guid id);
        List<CarEntity> GetAll();
        void Remove(Guid id);
    }
    public class CarRepository : ICarRepository
    {
        private readonly Data data;
        private readonly IMapper _mapper;

        public CarRepository(Data data, IMapper mapper)
        {
            this.data = data;
            _mapper = mapper;
        }

        public void Create(CarCreateEntity car)
        {
            CarEntity newEntity = _mapper.Map<CarEntity>(car);
            newEntity.Id = Guid.NewGuid();

            data.Cars.Add(newEntity);
        }

        public void Update(CarCreateEntity car, Guid id)
        {
            var updateEntity = data.Cars.FirstOrDefault(x => x.Id == id);

            data.Cars.Remove(updateEntity);

            CarEntity newEntity = _mapper.Map<CarEntity>(car);
            newEntity.Id = id;

            data.Cars.Add(newEntity);
        }

        public CarEntity GetById(Guid id)
        {
            return data.Cars.FirstOrDefault(x => x.Id == id);
        }

        public void Remove(Guid id)
        {
            var removeEntity = data.Cars.FirstOrDefault(x => x.Id == id);
            data.Cars.Remove(removeEntity);
        }

        public List<CarEntity> GetAll()
        {
            return data.Cars;
        }
    }
}
