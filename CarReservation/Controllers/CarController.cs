using CarReservation.ViewModels;
using CarWorker;
using Microsoft.AspNetCore.Mvc;

namespace CarReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarWorker _carWorker;

        public CarController(ICarWorker carWorker)
        {
            _carWorker = carWorker;
        }

        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string GetById([FromQuery] int id)
        {
            return "value";
        }

        [HttpPost]
        public void Create([FromBody] CarCreateViewModel model)
        {
           
        }

        [HttpPut("{id}")]
        public void Put([FromQuery] int id, [FromBody] CarCreateViewModel value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
