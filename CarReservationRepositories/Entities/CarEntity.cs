namespace CarReservationRepositories.Entities
{
    public class CarEntity
    {
        public Guid Id { get; set; }

        public static implicit operator CarEntity(CarCreateEntity v)
        {
            throw new NotImplementedException();
        }
    }

    public class CarCreateEntity
    {
        public string Model { get; set; }
        public string Number { get; set; }
    }
}
