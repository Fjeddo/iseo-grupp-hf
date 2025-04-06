namespace RivaRental.Domain;

public interface IBookingRepository
{
    void Add(Booking booking);
}

public class BookingRepository : IBookingRepository
{
    private readonly List<Booking> _bookings = new();

    public void Add(Booking booking)
    {
        _bookings.Add(booking);
    }
}