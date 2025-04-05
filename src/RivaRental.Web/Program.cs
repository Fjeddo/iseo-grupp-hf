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

app.MapPost("/rent", (RentRequest rentRequest, RentalService service) =>
{
    var result = service.TryBook(rentRequest.BoatType);

    return result.additional 
        ? Results.Accepted("/book-extra", result.boat) 
        : Results.Ok(result.boat);
});

app.MapPost("/book-extra", () => "Extra!");

app.MapPost("/return", (ReturnRequest returnRequest, RentalService service) => "Return!");

app.Run();

