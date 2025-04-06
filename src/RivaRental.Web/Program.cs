using System.Runtime.InteropServices.JavaScript;
using RivaRental.Domain;
using RivaRental.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<PriceCalculator>();
builder.Services.AddSingleton<RentalService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/rent", (RentRequest rentRequest, RentalService service, HttpContext ctx) =>
{
    var result = service.TryBook(rentRequest.BoatType);

    if (result.additional)
    {
        ctx.Response.Headers.Append("extra-possible", "true");
    }

    return Results.AcceptedAtRoute("Booking", new Booking
    {
        BookingNumber = $"{DateTimeOffset.UtcNow.UtcTicks}_{result.boat.GetType().Name}",
        Boat = new IseoSuper { },
        StartTime = DateTime.Now,
        StartEngineHours = 42

    });
});

app.MapGet("/booking", () => "Extra!").WithName("Booking");

app.MapPost("/return", (ReturnRequest returnRequest, RentalService service) =>
{
    var price = service.ReturnBoatAndGetTotalPrice(returnRequest.BookingNumber);
    
    return new ReturnBoatResponse { TotalPrice = price };
});

app.Run();

;