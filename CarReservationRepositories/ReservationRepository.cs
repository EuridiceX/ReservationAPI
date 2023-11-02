using AutoMapper;
using CarReservationRepositories.Entities;

namespace CarReservationRepositories
{
    public interface IReservationRepository
    {
        void Create(ReservationCreateEntity car);
        List<ReservationEntity> GetAll();
    }
    public class ReservationRepository : IReservationRepository
    {
        private readonly Data data;
        private readonly IMapper _mapper;

        public ReservationRepository(Data data, IMapper mapper)
        {
            this.data = data;
            _mapper = mapper;
        }

        public void Create(ReservationCreateEntity reservation)
        {
            ReservationEntity newEntity = _mapper.Map<ReservationEntity>(reservation);
            newEntity.Id = Guid.NewGuid();

            data.Reservations.Add(newEntity);

        }

        public List<ReservationEntity> GetAll()
        {
            return data.Reservations;
        }
    }
}
