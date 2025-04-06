namespace RivaRental.Web;

public record RentRequest(string BoatType, DateTimeOffset StartTime)
{
}

public record ReturnRequest(string BookingNumber);
public record BookExtraRequest(string BookingNumber, Extra Extra);
public record Extra(int NumberOfCrew);