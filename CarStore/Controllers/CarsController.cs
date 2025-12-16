using Microsoft.AspNetCore.Mvc;
namespace CarStore.Services;

public class CarsController : Controller
{
    private readonly CarService service;

    public CarsController(CarService service)
    {
        this.service = service;
    }

    public async Task<IActionResult> Index()
    {
        return View(await service.GetCarsAsync());
    }
}
