using BMAPI.Domain.Dijkstra;
using BMAPI.Domain.Entities;
using BMAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMAPI.Infrastructure.Repository
{
    public class TravelRouteRepository
    {
        private readonly BMContext _context;

        public TravelRouteRepository(BMContext context)
        {
            _context = context;
        }

        public async Task<List<TravelRoute>> ListRoutes()
        {
            List<TravelRoute> list = await _context.TravelRoutes.ToListAsync();

            return list;
        }

        public async Task<TravelRoute> GetRoute(int routeId)
        {
            TravelRoute route = await _context.TravelRoutes.FindAsync(routeId);

            return route;
        }

        public async Task<TravelRoute> CreateRoute(TravelRoute route)
        {
            var ret = await _context.TravelRoutes.AddAsync(route);

            await _context.SaveChangesAsync();

            ret.State = EntityState.Detached;

            return ret.Entity;
        }

        public async Task<int> UpdateRoute(TravelRoute route)
        {
            _context.Entry(route).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteRoute(int routeId)
        {
            var item = await _context.TravelRoutes.FindAsync(routeId);
            _context.TravelRoutes.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<CheapestDistance>> CheapestRoute(string origem, string destino)
        {
            DirectedWeightedGraph g = new DirectedWeightedGraph();
            var vertOrigem = await _context.TravelRoutes.Select(p => p.Origem).ToListAsync();
            var vertDestino = await _context.TravelRoutes.Select(p => p.Destino).ToListAsync();
            var x = vertOrigem.Union(vertDestino).Distinct().ToList();
            foreach (var item in x)
            {
                g.InsertVertex(item);
            }
            var edgeValue = _context.TravelRoutes.ToList();
            foreach (var item in edgeValue)
            {
                g.InsertEdge(item.Origem, item.Destino, item.Valor);
            }
            var res = g.FindPaths(origem);
            var resultado = res.Where(o => o.Destino == destino).ToList();
            return resultado;
        }
    }
}
