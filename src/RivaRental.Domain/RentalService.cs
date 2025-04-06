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

    private (bool, string) TryRent(Boat boat, DateTimeOffset startTime)
    {
        var booking = new Booking()
        {
            BookingNumber = DateTimeOffset.UtcNow.Ticks.ToString()
        };

        _bookingRepository.Add(booking);

        return (boat.CanIncludeAdditonalCosts, booking.BookingNumber);
    }

    public void Return(string bookingNumber)
    {

    }
}