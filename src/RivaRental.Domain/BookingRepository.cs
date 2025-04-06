namespace RivaRental.Domain;

public class BookingRepository
{
    private readonly List<Booking> _bookings = new List<Booking>();

    public void Add(Booking booking)
    {
        _bookings.Add(booking);
    }
}