using AutoMapper;
using CarReservation.ViewModels;
using CarReservationWorkers.Models;
using CarReservationWorker;
using Microsoft.AspNetCore.Mvc;
using CarReservationAPI.Controllers;

namespace CarReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarWorker _carWorker;
        private readonly IMapper _mapper;

        public CarController(ICarWorker carWorker, IMapper mapper)
        {
            _carWorker = carWorker;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<CarViewModel>> GetAll()
        {
            var carModels = await _carWorker.GetAll();
            return _mapper.Map<List<CarViewModel>>(carModels);
        }

        [HttpGet("{id}")]
        public async Task<CarViewModel> GetById( Guid id)
        {
          var carModel =  await _carWorker.GetById(id);
          return _mapper.Map<CarViewModel>(carModel);
        }

        [HttpPost]
        public  async Task<IActionResult> Create([FromBody] CarCreateViewModel viewModel)
        {
            var model =  _mapper.Map<CarCreateModel>(viewModel);
            var result = await _carWorker.Create(model);

            return this.GetResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CarCreateViewModel viewModel)
        {
            var model = _mapper.Map<CarCreateModel>(viewModel);
            var result = await _carWorker.Update(model,id);
           
            return this.GetResult(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
             await _carWorker.Remove(id);
        }
    }
}
