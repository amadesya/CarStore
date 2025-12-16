using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using CarShopRedis.Models;

namespace CarShopRedis.Controllers;

public class CarsController : Controller
{
    private readonly IDatabase _db;

    public CarsController(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public IActionResult Index()
    {
        var cars = new List<Car>();

        for (int i = 1; i <= 5; i++)
        {
            var data = _db.HashGetAll($"car:{i}");
            cars.Add(new Car
            {
                Id = i,
                Brand = data.First(x => x.Name == "Brand").Value,
                Model = data.First(x => x.Name == "Model").Value,
                Year = int.Parse(data.First(x => x.Name == "Year").Value),
                Price = int.Parse(data.First(x => x.Name == "Price").Value)
            });
        }

        return View(cars);
    }
}
