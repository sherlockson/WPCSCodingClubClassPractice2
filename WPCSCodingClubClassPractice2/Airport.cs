public class Airport
{
    //Class Variables
    public string name;
    public int ramps;
    private int capacity;

    //Constructor
    public Airport(string name, int ramps)
    {
        this.name = name;
        this.ramps = ramps;
        this.capacity = new Random().Next(70, 150);
    }

    //A method to try and fill the airplane with the number of passengers available at the airport
    public int FillAirplane(Airplane plane)
    {
        //If the plane capacity does not put the airport capacity below zero, it can fill the whole plane
        if (capacity - plane.seats > 0)
        {
            plane.passengerCount = plane.seats;
            return plane.seats;
        }
        //But if it takes it below zero, we only fill up with what we have left
        else if (capacity - plane.seats < 0)
        {
            plane.passengerCount = capacity;
            int actualPassengers = capacity;
            capacity = 0;
            return actualPassengers;
        }
        //If the airport has zero passengers, uh oh
        else
        {
            plane.passengerCount = 0;
            return 0;
        }
    }

    //A function to take the passengers off the plane and add them back to the airport's capacity to get on another flight
    public void DeboardPlane(Airplane plane)
    {
        capacity += plane.passengerCount;
        plane.passengerCount = 0;
        plane.passengers.Clear();
        plane.seatAssignments.Clear();
        foreach (var p in plane.passengers)
        {
            p.ResetPassenger();
        }
        plane.RefillSnacks();
    }

    //Land the plane at the airport if Ramps are available
    public bool LandPlane()
    {
        if (ramps != 0)
        {
            ramps--;
            return true;
        }
        else
        {
            return false;
        }
    }
}