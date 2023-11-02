using CarReservationRepositories.Entities;

namespace CarReservationRepositories
{
    public interface ICarRepository
    {
        void Create(CarCreateEntity car);
        void Update(CarCreateEntity car, Guid id);
        CarEntity GetById();
        void Remove(Guid id);
    }
    public class CarRepository : ICarRepository
    {
        private readonly Data data;

        public CarRepository(Data data)
        {
            this.data = data;
        }

        public void Create(CarCreateEntity car)
        {
            data.Cars.Add(car);
        }

        public void Update(CarCreateEntity car, Guid id)
        {
            var updateEntity = data.Cars.FirstOrDefault(x => x.Id == id);

            data.Cars.Remove(updateEntity);

            CarEntity newEntity = car;
            newEntity.Id = id;  

            data.Cars.Add(newEntity);
        }

        public CarEntity GetById()
        {
            return data.Cars.FirstOrDefault();
        }

        public void Remove(string car)
        {
            data.Cars.Remove(car);
        }

    }
}
