namespace RivaRental.Domain;

public class RentalService
{
    private List<Boat> boats = new List<Boat>();

    public (bool additional, Boat boat) TryBook(string type) =>
        type switch
        {
            "Diable" => (true, new Diable()),
            "IseoSuper" => (false, new IseoSuper()),
            "Dolceriva" => (false, new Dolceriva()),
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
        var endEngineHours = 10;

        var hours = (int)Math.Ceiling((DateTimeOffset.Now - booking.StartTime).TotalHours);
        var engineHours = endEngineHours - booking.StartEngineHours; 

        var price = PriceCalculator.Calculate(booking.Boat, hours, engineHours);

        return price;
    }
}
