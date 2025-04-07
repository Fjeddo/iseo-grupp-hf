namespace RivaRental.Domain;

public class PriceCalculator
{
    public static double Calculate(Boat b, int hours, double walkingHours, double extra = 0) =>
        b switch
        {
            Diable diable => diable.CalculatePrice(hours, walkingHours) + extra,
            _ => b.CalculatePrice(hours, walkingHours)
        };
}