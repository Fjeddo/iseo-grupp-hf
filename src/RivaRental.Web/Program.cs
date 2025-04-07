using RivaRental.Domain;
using RivaRental.Web;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    WebRootPath = "rest/api"
});

builder.Services.AddOpenApi();
builder.Services.AddSingleton<PriceCalculator>();
builder.Services.AddSingleton<IBookingRepository, BookingRepository>();
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
    var result = service.TryRent(
        rentRequest.BoatType,
        rentRequest.StartTime,
        rentRequest.StartWalkingHours);

    if (result.additional)
    {
        ctx.Response.Headers.Append("extra-possible", "true");
    }

    //return Results.AcceptedAtRoute("Booking", routeValues: result.bookingNumber);
    return Results.AcceptedAtRoute("Booking", new { BookingNumber = result.bookingNumber });

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


restApi.MapPost("/return", (ReturnRequest returnRequest, RentalService service) =>
{
    var price = service.ReturnBoatAndGetTotalPrice(returnRequest.BookingNumber, returnRequest.EndWalkingHours);

    return new ReturnBoatResponse { TotalPrice = price };
});

restApi.MapPost("/return", (ReturnRequest returnRequest, RentalService service) => "Return!");

app.Run();

public partial class Program
{

}