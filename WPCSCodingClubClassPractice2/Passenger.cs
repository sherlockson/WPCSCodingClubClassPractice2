public class Passenger
{
    public string name;
    public int flightNumber;
    public string seat;
    public Airplane airplane;

    public Passenger(string name, int flightNumber, Airplane airplane)
    {
        this.name = name;
        this.flightNumber = flightNumber;
        this.airplane = airplane;
    }

    public void GetSnack()
    {
        if (airplane.GiveSnack() == 1)
        {
            Console.WriteLine("Snack Time!");
        }
        else
        {
            Console.WriteLine("Sad! No more snacks...");
        }
    }

    public void GetDrink(string request)
    {
        int success = airplane.GiveDrink(request);
        if (success == 1)
        {
            Console.WriteLine("They have my drink! I ordered a " + request);
        }
        else
        {
            Console.WriteLine("Sad! They don't have my drink... I wanted a " + request);
        }
    }

    public void ResetPassenger()
    {
        this.seat = "";
    }
}