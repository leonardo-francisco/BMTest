using BMAPI.Domain.Entities;
using BMAPI.Infrastructure.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMAPI.Domain.Services
{
    public class TravelRouteService
    {
        private readonly TravelRouteRepository _travelRouteRepository;
        public TravelRouteService(TravelRouteRepository travelRouteRepository)
        {
            _travelRouteRepository = travelRouteRepository;
        }

        public async Task<List<TravelRoute>> ListRoutes()
        {
            List<TravelRoute> list = await _travelRouteRepository.ListRoutes();

            return list;
        }

        public async Task<TravelRoute> GetRoute(int routeId)
        {
            TravelRoute route = await _travelRouteRepository.GetRoute(routeId);

            return route;
        }

        public async Task<TravelRoute> CreateRoute(TravelRoute route)
        {
            route = await _travelRouteRepository.CreateRoute(route);

            return route;
        }

        public async Task<int> UpdateRoute(TravelRoute route)
        {
            return await _travelRouteRepository.UpdateRoute(route);
        }

        public async Task<bool> DeleteRoute(int routeId)
        {
            TravelRoute fndRoute = await _travelRouteRepository.GetRoute(routeId);

            await _travelRouteRepository.DeleteRoute(routeId);

            return true;
        }

        public async Task<List<CheapestDistance>> CheapestRoute(string origem, string destino)
        {
            List<CheapestDistance> cheapest =  await _travelRouteRepository.CheapestRoute(origem, destino);

            return cheapest;
        }
    }
}
