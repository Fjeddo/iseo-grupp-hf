using RivaRental.Domain;

public record Booking
{
    public Guid Id { get; } = Guid.NewGuid();
    public required string BookingNumber { get; set; }
    //public required string RegistrationNumber { get; set; }
    //public required string CustomerId { get; set; }
    public required Boat Boat { get; set; }
    public required DateTimeOffset StartTime { get; set; }
    //public DateTimeOffset? EndTime { get; set; }
    public required double StartEngineHours { get; set; }
    public int? EndEngineHours { get; set; }
    //public required bool IsActive { get; set; } = true;
}