using MySql.Data.MySqlClient;
using StackExchange.Redis;
using System.Data;
using System.Text.Json;
using CarStore.Models;

public class CarService
{
    private readonly IDatabase redis;
    private readonly MySqlConnection mysql;

    public CarService(IConnectionMultiplexer multiplexer, MySqlConnection connection)
    {
        redis = multiplexer.GetDatabase();
        mysql = connection;
    }

    public async Task<List<Car>> GetCarsAsync()
    {
        var cached = await redis.StringGetAsync("cars:all");

        if (cached.HasValue)
            return JsonSerializer.Deserialize<List<Car>>(cached);

        var cars = new List<Car>();

        await mysql.OpenAsync();
        var cmd = new MySqlCommand("SELECT * FROM cars", mysql);
        var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            cars.Add(new Car
            {
                Id = reader.GetInt32("id"),
                Brand = reader.GetString("brand"),
                Model = reader.GetString("model"),
                Year = reader.GetInt32("year"),
                Price = reader.GetInt32("price")
            });
        }

        await mysql.CloseAsync();

        await redis.StringSetAsync(
            "cars:all",
            JsonSerializer.Serialize(cars),
            TimeSpan.FromMinutes(5)
        );

        return cars;
    }
}
