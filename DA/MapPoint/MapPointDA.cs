namespace MinecraftTrackerApp.DA.MapPoint
{
    using Dapper;
    using System.Threading.Tasks;
    using MinecraftTrackerObjects;
    using Microsoft.Data.SqlClient;

    public class MapPointDA
    {
        private readonly string _connectionString;

        public MapPointDA(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<MapPoint>> GetAllMapPointsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT MapPointId, Name, Coordinates, Position FROM MapPoint";

                // QueryAsync<T> is a Dapper method to run a query and map the result to a list of T
                var mapPoints = await connection.QueryAsync<MapPoint>(sql);

                // Convert the result to a List<MapPoint>
                return mapPoints.AsList();
            }
        }

        public async Task AddMapPointAsync(MapPoint mapPoint)
        {
            var sql = "INSERT INTO MapPoint (MapPointId, Name, Coordinates, Position) VALUES (NEWID(), @Name, @Coordinates, @Position)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sql, new { mapPoint.Name, mapPoint.Coordinates, mapPoint.Position });
            }
        }
    }
}
