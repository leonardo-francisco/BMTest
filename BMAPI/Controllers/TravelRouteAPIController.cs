using BMAPI.Domain.Entities;
using BMAPI.Domain.Services;
using BMAPI.Infrastructure.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelRouteAPIController : ControllerBase
    {       
        private readonly TravelRouteService _travelRoutService;
        private IValidator<TravelRoute> _validator;

        public TravelRouteAPIController(TravelRouteService travelRoutService, IValidator<TravelRoute> validator)
        {
            _travelRoutService = travelRoutService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelRoute>>> Get()
        {
            var model = await _travelRoutService.ListRoutes();
            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _travelRoutService.GetRoute(id);

            if (model == null) return NotFound();

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TravelRoute travelRoute)
        {
            ValidationResult result = await _validator.ValidateAsync(travelRoute);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState, null);
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            await _travelRoutService.CreateRoute(travelRoute);

            return Ok(travelRoute);
        }

        [HttpPatch]
        public async Task<IActionResult> Put(TravelRoute travelRoute)
        {
            var model = await _travelRoutService.GetRoute(travelRoute.Id);

            if (model == null) return NotFound();

            ValidationResult result = await _validator.ValidateAsync(travelRoute);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState, null);
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            model.Id = travelRoute.Id;
            model.Origem = travelRoute.Origem;
            model.Destino = travelRoute.Destino;
            model.Valor = travelRoute.Valor;

            await _travelRoutService.UpdateRoute(model);

            return Ok(model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _travelRoutService.GetRoute(id);

            if (model == null) return NotFound();

           await _travelRoutService.DeleteRoute(id);

            return NoContent();
        }

        [HttpPost("/CheapestRoute/{origem}/{destino}")]
        public async Task<IActionResult> CheapestRoute(string origem, string destino)
        {
            var resultado = await _travelRoutService.CheapestRoute(origem,destino);
            return Ok(resultado);
        }
    }
}
