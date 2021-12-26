public class WPCSCodingClubClassPractice2
{
    /*
     * Some things to note about this code:
     * 1. We have not gone over everything you will see. We will spend the next meeting or two going over things written here
     * 2. This is verbose code. I wrote out things so you could see how they are made and interact
     * 3. Read the comments throughout to get an idea about how this code works.
     * 4. The point of this simulation is to demonstrate a number of custom objects interacting with eachother
     * 5. It's okay if you don't understand what is going on with this code! You will after we go through it!
     */
    public static void Main(string[] args)
    {
        //Set the number of seats on the flight
        int seats = 60;

        //A list of names for passengers
        string[] names = new string[] { "Andrew", "Michael", "John", "Mary", "Richard", "Molly", "Emily", "David", "Travis", "Caroline" };

        //Creating the airport
        Airport DallasFtWorth = new Airport("Dallas Ft Worth", 2);
        Airport Dayton = new Airport("Dayton", 2);
        List<Airport> availableAirports = new List<Airport>();
        availableAirports.Add(Dayton);
        availableAirports.Add(DallasFtWorth);



        //Creating the airplane
        //Remember... These need to match the constructor we made in the Airplane class!
        Airplane AA4006 = new Airplane(4006, seats, "Embraer", "American", DallasFtWorth);
        Airplane UL7762 = new Airplane(7762, seats, "Boeing", "United", Dayton);
        Airplane DL811 = new Airplane(811, seats, "Airbus", "Delta", Dayton);
        List<Airplane> airplanes = new List<Airplane>();
        airplanes.Add(AA4006);
        airplanes.Add(UL7762);
        airplanes.Add(DL811);

        //Make a random number generator for testing later
        Random rand = new Random();

        //Request passengers for each of the flights
        AA4006.RequestPassengers(AA4006.currentAirport);
        UL7762.RequestPassengers(UL7762.currentAirport);
        DL811.RequestPassengers(DL811.currentAirport);

        foreach (var plane in airplanes)
        {
            for (int i = 0; i < plane.passengerCount; i++)
            {
                //Create a passenger object. We call it "temp" here because the name of the object doesn't matter once it's in the list
                Passenger temp = new Passenger(names[rand.Next(names.Count())], plane.flightNumber, plane);

                //Now, we add the passenger to the passengers list for the airplane
                plane.passengers.Add(temp);
                //Then, we assign the passenger a seat on our airplane
                plane.AssignSeat(temp);
            }
            plane.TakeOff();
        }

        //For each loops are useful for iterating (going over) objects in a list. We will talk about them more when we meet
        foreach (var plane in airplanes)
        {
            foreach (var p in plane.passengers)
            {
                Console.WriteLine("Passenger " + p.name + " on Flight " + plane.flightNumber + " has seat " + p.seat);
            }

            //Now, let's change some passengers seats around. Let's cap the amount of seat changes to be no more than 25% of the plane
            for (int i = 0; i < rand.Next(plane.seats / 4); i++)
            {
                //We will randomly select the seats that will change
                int toChange1 = rand.Next(plane.passengerCount);
                int toChange2 = rand.Next(plane.passengerCount);
                //Then, we call our function to change the seats and print out the change
                plane.ChangeSeat(plane.passengers[toChange1], plane.passengers[toChange2]);
                Console.WriteLine("Passenger on flight " + plane.flightNumber + " at seat " + plane.passengers[toChange1].seat + " changed seats to " + plane.passengers[toChange2].seat);
            }

            //Now, we will try and give all the passengers a snack. But some might be dissapointed!
            foreach (var p in plane.passengers)
            {
                p.GetSnack();
            }

            //A list of possible strings of drinks
            string[] possibleDrinks = new string[] { "Coke", "Pepsi", "Sprite", "Fanta", "Coffee", "Water", "Dr. Pepper", "Ginger Ale" };

            //After that snack, some passengers might be thirsty. We will randomly see which ones are and try to get them a drink
            foreach (var p in plane.passengers)
            {
                //Thirsty will generate a random number between 0 and 1. If it is 1, they are thirsy
                int thirsy = rand.Next(2);
                if (thirsy == 1)
                {
                    //We will then try and get them a random list from the list, if it's possible.
                    p.GetDrink(possibleDrinks[rand.Next(rand.Next(possibleDrinks.Count()))]);
                }
            }

            //Try and land at the other airport we created
            if (plane.currentAirport == DallasFtWorth)
            {
                plane.LandPlane(Dayton);
            }
            else
            {
                plane.LandPlane(DallasFtWorth);
            }
        }

    }
}