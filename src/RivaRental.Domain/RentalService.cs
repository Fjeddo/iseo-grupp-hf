namespace RivaRental.Domain;

public class RentalService
{
    private List<Boat> boats = new List<Boat>();


    public (bool additional, Boat boat) TryBook(string type) =>
        type switch
        {
            "Diable" => (true, new Diable()),
            "IseoSuper" => (false, new IseoSuper()),
            "Dolceriva" => (false, new Dolceriva()),
            _ => throw new NotSupportedException()
        };

    public void Return(string bookingNumber)
    {

    }
}