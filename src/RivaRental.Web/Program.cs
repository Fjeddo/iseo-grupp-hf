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
    var (additional, boat) = service.TryBook(rentRequest.BoatType);

    if (additional)
    {
        ctx.Response.Headers.Append("extra-possible", "true");
    }
    
    return Results.AcceptedAtRoute("Booking", new Booking
        {
            BookingNumber = $"{DateTimeOffset.UtcNow.UtcTicks}_{boat.GetType().Name}",
        });
});

app.MapGet("/booking", () => "Extra!").WithName("Booking");

app.MapPost("/return", (ReturnRequest returnRequest, RentalService service) => "Return!");

app.Run();

