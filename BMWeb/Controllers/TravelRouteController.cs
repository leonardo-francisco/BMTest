using BMWeb.DTO;
using BMWeb.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWeb.Controllers
{
    public class TravelRouteController : Controller
    {
        private readonly TravelRouteAPIService _apiService;
        private IValidator<TravelRouteDTO> _validator;

        public TravelRouteController(TravelRouteAPIService apiService, IValidator<TravelRouteDTO> validator)
        {
            _apiService = apiService;
            _validator = validator;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _apiService.GetAllRoutes();

            return View(model);
        }

        public IActionResult CreateRoute()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoute(TravelRouteDTO routeDTO)
        {
            ValidationResult result = _validator.Validate(routeDTO);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState, null);
                return View("CreateRoute");
            }

            var model = new TravelRouteDTO()
            {
                Origem = routeDTO.Origem,
                Destino = routeDTO.Destino,
                Valor = routeDTO.Valor               
            };
            await _apiService.CreateRoute(model);
            TempData["crtSucc"] = "Rota cadastrada com sucesso!";
            return RedirectToAction("Index", "TravelRoute");
        }

        public async Task<IActionResult> EditRoute(int id)
        {
            var model = await _apiService.GetRouteById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRoute(int id, TravelRouteDTO routeDTO)
        {
            ValidationResult result = _validator.Validate(routeDTO);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState, null);
                return View("EditRoute");
            }

            var model = new TravelRouteDTO()
            {
                Id = routeDTO.Id,
                Origem = routeDTO.Origem,
                Destino = routeDTO.Destino,
                Valor = routeDTO.Valor
            };
            await _apiService.UpdateRoute(model);
            TempData["edtSucc"] = "Rota editada com sucesso!";
            return RedirectToAction("Index", "TravelRoute");
        }

        [HttpDelete]
        public void DeleteRoute(int id)
        {
            _apiService.DeleteRoute(id);
        }

        public IActionResult CheapestRoute()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheapestRoute(string origem, string destino)
        {
            var model = await _apiService.CheapestRoute(origem, destino);
            ViewBag.PesqRotas = model;
            return View();
        }
    }
}
