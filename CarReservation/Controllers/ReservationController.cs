using AutoMapper;
using CarReservationAPI.Extensions;
using CarReservationAPI.ViewModels;
using CarReservationWorker;
using CarReservationWorkers.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationWorker _reservationWorker;
        private readonly IMapper _mapper;

        public ReservationController(IReservationWorker reservationWorker, IMapper mapper)
        {
            _reservationWorker = reservationWorker;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<ReservationViewModel>> GetAll()
        {
            var reservations = await _reservationWorker.GetAll();
            return _mapper.Map<List<ReservationViewModel>>(reservations);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReservationCreateViewModel viewModel)
        {
            var model = _mapper.Map<ReservationCreateModel>(viewModel);
            var result = await _reservationWorker.Create(model);

            return this.GetResult(result);
        }

    }
}
