using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using MinecraftTrackerApp.DA.MapPoint;
using MinecraftTrackerObjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MinecraftTrackerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapPointController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        MapPointDA mpDA;

        public MapPointController(IConfiguration configuration)
        {
            _configuration = configuration;
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            mpDA = new MapPointDA(connectionString);
        }

        // GET api/<MapPointController>/5
        [HttpGet("GetAllPoints")]
        public async Task<List<MapPoint>> GetAllPoints()
        {
            List<MapPoint> points;
            try
            {
                points = await mpDA.GetAllMapPointsAsync();
            }
            catch (Exception ex)
            {
                points = new List<MapPoint>();
            }
            return points;
        }

        // POST api/<MapPointController>
        [HttpPost]
        public async Task<string> Post([FromBody] MapPoint mapPoint)
        {
            try
            {
                await mpDA.AddMapPointAsync(mapPoint);
            }
            catch (Exception ex)
            {

            }

            return mapPoint.Coordinates;
        }
    }
}
