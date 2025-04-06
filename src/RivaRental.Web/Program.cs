using RivaRental.Domain;
using RivaRental.Web;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    WebRootPath = "rest/api"
});

builder.Services.AddOpenApi();
builder.Services.AddSingleton<PriceCalculator>();
builder.Services.AddSingleton<RentalService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var restApi = app.MapGroup("rest/api");

restApi.MapPost("/rent", (RentRequest rentRequest, RentalService service, HttpContext ctx) =>
{
    var (additional, boat) = service.TryBook(rentRequest.BoatType);

    if (additional)
    {
        ctx.Response.Headers.Append("extra-possible", "true");
    }
    
    return Results.AcceptedAtRoute("Booking", new
        {
            BookingNumber = $"{DateTimeOffset.UtcNow.UtcTicks}_{boat.GetType().Name}",
        });
});

restApi.MapGet("/booking", (string bookingNumber, HttpContext ctx) =>
{
    if (ctx.Request.Headers.Accept.Any(s => s.Contains("image") && s.Contains("png")))
    {
        if (bookingNumber == "1")
        {
            return Results.File(File.ReadAllBytes("static/diable.png"), contentType: "image/png");
        }

        if (bookingNumber == "2")
        {
            return Results.File(File.ReadAllBytes("static/dolceriva.png"), contentType: "image/png");
        }

        if (bookingNumber == "3")
        {
            return Results.File(File.ReadAllBytes("static/iseosuper.png"), contentType: "image/png");
        }
    }

    return Results.Ok("Extra!");
}).WithName("Booking");

restApi.MapPost("/return", (ReturnRequest returnRequest, RentalService service) => "Return!");

app.Run();

