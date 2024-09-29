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
        MapPointDA mpDA = new MapPointDA("Server=minecraft.cpesk00qmvaq.us-east-1.rds.amazonaws.com;Database=Minecraft;User Id=admin;Password=FlnJhLbAqs8Ij0VLtRCO;TrustServerCertificate=True;MultipleActiveResultSets=True;");


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
