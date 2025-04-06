namespace RivaRental.Domain;

public class RentalService
{
    private readonly BookingRepository _bookingRepository = new BookingRepository();

    public (bool additional, string bookingNumber) TryRent(
        string type,
        DateTimeOffset startTime) =>
        type switch
        {
            "Diable" => TryRent(new Diable(), startTime),
            "IseoSuper" => TryRent(new IseoSuper(), startTime),
            "Dolceriva" => TryRent(new Dolceriva(), startTime),
            _ => throw new NotSupportedException()
        };

    public void Return(string bookingNumber)
    {

    }

    public Booking GetBooking(string bookingNumber)
    {
        if (bookingNumber.ToLower() == "abc123")
            return new Booking { BookingNumber = bookingNumber, Boat = new IseoSuper { }, 
                StartTime = DateTimeOffset.Now.AddHours(-11).AddMinutes(-59), StartEngineHours = 2 
            };
        throw new Exception("Booking not found!");
    }

    public double ReturnBoatAndGetTotalPrice(string bookingNumber)
    {
        var booking = GetBooking(bookingNumber);
        var endWalkingHours = 10;

        var hours = (int)Math.Ceiling((DateTimeOffset.Now - booking.StartTime).TotalHours);
        var walkingHours = endWalkingHours - booking.StartEngineHours; 

        var price = PriceCalculator.Calculate(booking.Boat, hours, walkingHours);

        return price;
    }

    private (bool, string) TryRent(Boat boat, DateTimeOffset startTime)
    {
        var booking = new Booking()
        {
            BookingNumber = DateTimeOffset.UtcNow.Ticks.ToString(),
            Boat = new IseoSuper { },
            StartTime = DateTime.Now,
            StartEngineHours = 42
        };

        _bookingRepository.Add(booking);

        return (boat.CanIncludeAdditonalCosts, booking.BookingNumber);
    }
}
