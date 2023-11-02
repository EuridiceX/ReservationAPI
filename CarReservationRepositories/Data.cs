using CarReservationRepositories.Entities;

namespace CarReservationRepositories
{
    public class Data
    {
        public List<CarEntity> Cars { get; set; }

        public Data()
        {
            Cars = new List<CarEntity>();
        }
    }

}
