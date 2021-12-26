public class Airplane
{
    //Class Variables
    public int seats;
    public int passengerCount;
    public int flightNumber;
    public string name;
    public string airline;
    public List<Passenger> passengers;
    public List<string> seatAssignments; //A List object containing strings
    private int snacks;
    private string[] drinkList = new string[] {"Coke", "Pepsi", "Sprite", "Dr. Pepper", "7-up", "Water", "Coffee"};
    public Airport currentAirport;

    //Constructor. Notice I am initializing the lists even though we are not assigning anything to them yet
    public Airplane(int flightNumber, int seats, string name, string airline, Airport currentAirport)
    {
        this.flightNumber = flightNumber;
        this.seats = seats;
        this.name = name;
        this.airline = airline;
        this.seatAssignments = new List<string>();
        this.passengers = new List<Passenger>();
        this.snacks = seats / 2;
        this.currentAirport = currentAirport;
    }

    /*
     * Here is a method to assign everyone a seat on our plane. 
     * We are assuming there are 6 people in a row, becasue we are searching for the 6th letter of the alphabet 'G'
     */
    public void AssignSeat(Passenger passenger)
    {
        if (String.IsNullOrEmpty(passenger.seat)) //Check and see if this passenger already has a seat
        {
            if (seatAssignments.Count == 0) //Check and see if this is the first seat assignment
            {
                //If it is, we start at seat "1A" and add it to the list
                passenger.seat = "1A";
                seatAssignments.Add(passenger.seat);
                return;
            }

            //If this is not the first seat, we now need to see if the last assignment was the last in a row
            else
            {
                //First, we get the last seat that was added
                string tempSeat = seatAssignments.Last();

                //Then, we check if it has "G" in it
                if (tempSeat.Contains("G"))
                {
                    //If it does, we need to start a new row, so we remove "G" from the string
                    tempSeat = tempSeat.Remove(1, 1);

                    //Then, we increase the row number by 1 and add "A" to it
                    int rowNum = (int.Parse(tempSeat) + 1);
                    string newSeat = rowNum + "A";

                    //Then, we give the passenger their seat number
                    passenger.seat = newSeat;
                    seatAssignments.Add(newSeat);
                }
                //If this seat is not the last in the row, we only need to get the next character
                else
                {
                    //We change our string to an array of characters
                    char[] seat = tempSeat.ToCharArray();

                    //Select the letter from the seat (remember, we start counting from 0!)
                    char seatLetter = seat[1];

                    //We then add one to the character to get the next character
                    //This seems weird, and contradictory to everything that I've said so far...
                    //But all characters in programming are just UniCode characters (just numbers that convert to something else)
                    //We will talk about this more at our next meeting!
                    seat[1] = (char)(seatLetter + 1);
                    passenger.seat = new string(seat);
                    seatAssignments.Add(passenger.seat);
                }
            }
        }
    }

    //Here is a method to change a passenger's seat with someone else
    public void ChangeSeat(Passenger oldSeat, Passenger newSeat)
    {
        //Make a copy of the old seat
        string oldSeatNum = oldSeat.seat;

        //Set the old seat to the new seat
        oldSeat.seat = newSeat.seat;

        //Since we have overridden the old one, set the new one to the copy we made
        newSeat.seat = oldSeatNum;
    }

    //Give snacks to the hungry passengers
    public int GiveSnack()
    {
        if (snacks != 0)
        {
            //Snacks-- subtracts 1 from the snack count
            snacks--;
            return 1;
        }
        else
        {
            return 0;
        }
    }

    //Check and see if snack is available
    public bool IsSnackAvailable()
    {
        if (snacks != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Get the passengers a drink to quench their thirst
    public int GiveDrink(string request)
    {
        if (drinkList.Contains(request))
        {
            return 1;
        }
        else
        {
            return 0;
        }

    }

    //Land the plane at the airport. Once we do this, deboard and reset the aircraft for the mext flight
    public bool LandPlane(Airport airport)
    {
        if (airport.LandPlane())
        {
            Console.WriteLine("We landed at " + airport.name);
            airport.DeboardPlane(this);
            this.currentAirport = airport;
            Console.WriteLine("Flight " + this.flightNumber + " has deplaned at " + this.currentAirport);
            airport.ramps++;
            return true;
        }
        else
        {
            Console.WriteLine("We can't land at " + airport.name + ". They don't have enough ramps!");
            return false;
        }
    }

    //Request passengers from the airport the airplane is at.
    public void RequestPassengers(Airport airport)
    {
        Console.WriteLine("We are " + this.name + " and we are requesting " + this.seats + " passengers from " + airport.name);
        //If the airport can fully fill this plane, yay!
        if (airport.FillAirplane(this) == this.seats)
        {
            Console.WriteLine("We got all our seats filled!");
        }
        //If not, oh well
        else
        {
            Console.WriteLine("Uh oh. We could only get " + this.passengerCount + " passengers." );
        }
    }

    //Take off and free up a ramp at the airport
    public void TakeOff()
    {
        Console.WriteLine("Takeoff for flight " + this.airline + " " + this.flightNumber);
    }

    //Refill snacks for the next flight!
    public void RefillSnacks()
    {
        this.snacks = new Random().Next(seats);
        Console.WriteLine("Refilled Snacks with a random amount!");
    }

}