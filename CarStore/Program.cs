using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IConnectionMultiplexer>(
    //ConnectionMultiplexer.Connect("10.9.1.56,password=Password")
    ConnectionMultiplexer.Connect("192.168.0.106:6379,password=1")
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var redis = scope.ServiceProvider.GetRequiredService<IConnectionMultiplexer>();
    var db = redis.GetDatabase();

    if (!db.KeyExists("cars:initialized"))
    {
        db.HashSet("car:1", new HashEntry[] { new("Brand", "Toyota"), new("Model", "Camry"), new("Year", 2020), new("Price", 2500000) });
        db.HashSet("car:2", new HashEntry[] { new("Brand", "BMW"), new("Model", "X5"), new("Year", 2019), new("Price", 4500000) });
        db.HashSet("car:3", new HashEntry[] { new("Brand", "Audi"), new("Model", "A6"), new("Year", 2021), new("Price", 3800000) });
        db.HashSet("car:4", new HashEntry[] { new("Brand", "Mercedes"), new("Model", "E-Class"), new("Year", 2018), new("Price", 4200000) });
        db.HashSet("car:5", new HashEntry[] { new("Brand", "Kia"), new("Model", "K5"), new("Year", 2022), new("Price", 2700000) });
        db.StringSet("cars:initialized", true);
    }
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cars}/{action=Index}/{id?}");

app.Run();
