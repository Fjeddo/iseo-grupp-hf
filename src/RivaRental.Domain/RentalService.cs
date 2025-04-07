namespace RivaRental.Domain;

public class RentalService
{
    private readonly IBookingRepository _bookingRepository;

    public RentalService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public (bool additional, string bookingNumber) TryRent(
        string type,
        DateTimeOffset startTime,
        double startWalkingHours) =>
        type switch
        {
            "Diable" => TryRent(new Diable(), startTime, startWalkingHours),
            "IseoSuper" => TryRent(new IseoSuper(), startTime, startWalkingHours),
            "Dolceriva" => TryRent(new Dolceriva(), startTime, startWalkingHours),
            _ => throw new NotSupportedException()
        };

    public Booking GetBooking(string bookingNumber)
    {
        return _bookingRepository.Get(bookingNumber);
    }

    public double ReturnBoatAndGetTotalPrice(string bookingNumber, double endWalkingHours)
    {
        var booking = GetBooking(bookingNumber);

        var hours = (int)Math.Ceiling((DateTimeOffset.Now - booking.StartTime).TotalHours);
        var walkingHours = endWalkingHours - booking.StartEngineHours;

        var price = PriceCalculator.Calculate(booking.Boat, hours, walkingHours);

        return price;
    }

    private (bool, string) TryRent(
        Boat boat,
        DateTimeOffset startTime,
        double engineHours)
    {
        var booking = new Booking
        {
            BookingNumber = DateTimeOffset.UtcNow.Ticks.ToString(),
            Boat = boat,
            StartTime = startTime,
            StartEngineHours = engineHours
        };

        _bookingRepository.Add(booking);

        return (boat.CanIncludeAdditonalCosts, booking.BookingNumber);
    }
}
