using CarReservationWorkers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CarReservationAPI.Extensions
{
    public static class ControllerExtension
    {
        public static IActionResult GetResult(this ControllerBase controller, ValidationResult model)
        {
            if (model.Error != null)
            {
                if (model.Error.Type == HttpStatusCode.NotFound)
                {
                    return controller.NotFound(
                        new NotFoundObjectResult(model.Error.Message));
                }

                if (model.Error.Type == HttpStatusCode.BadRequest)
                {
                    return controller.NotFound(
                        new BadRequestObjectResult(model.Error.Message));
                }

            }

            return controller.Ok();
        }
    }
}
