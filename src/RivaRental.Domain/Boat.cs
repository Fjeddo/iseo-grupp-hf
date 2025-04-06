namespace RivaRental.Domain;

public abstract class Boat
{
    protected double BaseWorkingRate => 123.456;
    protected double BaseHourlyRate => 456.789;
    public virtual bool CanIncludeAdditonalCosts => false;

    internal abstract double CalculatePrice(int hours, int walkingHours);
    public double WorkingRate { get; }
    public double HourlyRate { get; }
    public string RegistrationNumber { get; }
}

public class IseoSuper : Boat
{
    internal override double CalculatePrice(int hours, int walkingHours)
        => BaseHourlyRate * hours + BaseWorkingRate * walkingHours;
}

public class Dolceriva : Boat
{
    internal override double CalculatePrice(int hours, int walkingHours)
        => BaseHourlyRate * hours * 1.3 + BaseWorkingRate * walkingHours * 1.5;
}

public class Diable : Boat
{
    public override bool CanIncludeAdditonalCosts => true;

    internal override double CalculatePrice(int hours, int walkingHours)
        => BaseHourlyRate * hours * 2.1 + BaseWorkingRate * walkingHours * 4;
}
