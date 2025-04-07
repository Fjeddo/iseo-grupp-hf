namespace RivaRental.Web;

public record RentRequest(string BoatType, DateTimeOffset StartTime, double StartWalkingHours);

public record ReturnRequest(string BookingNumber, double EndWalkingHours);

public record BookExtraRequest(string BookingNumber, Extra Extra);
public record Extra(int NumberOfCrew);