namespace RivaRental.Domain;

public interface IBookingRepository
{
    void Add(Booking booking);
    Booking Get(string bookingNumber);
}

public class BookingRepository : IBookingRepository
{
    private readonly List<Booking> _bookings = new();

    public void Add(Booking booking)
    {
        _bookings.Add(booking);
    }

    public Booking Get(string bookingNumber)
    {
        return _bookings
                   .FirstOrDefault(b => b.BookingNumber == bookingNumber)
                    ?? throw new Exception("Booking not found!");
    }
}