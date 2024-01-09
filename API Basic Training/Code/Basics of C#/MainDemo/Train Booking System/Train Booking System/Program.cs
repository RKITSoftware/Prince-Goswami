using System;
using System.Collections.Generic;
using System.Linq;

class Train
{
    public int TrainNumber { get; set; }
    public string StartingStation { get; set; }
    public string EndingStation { get; set; }
    public int Distance { get; set; }
    public Dictionary<string, List<int>> Coaches { get; set; } // CoachType, List of seat numbers
}

class TrainReservationSystem
{
    private List<Train> trains;
    private int lastPNR;

    public TrainReservationSystem()
    {
        trains = new List<Train>();
        lastPNR = 100000000;
    }

    public void PrintAvailableTrains()
    {
        if (trains.Count == 0)
        {
            Console.WriteLine("No trains available.");
            return;
        }

        Console.WriteLine("Available Trains:");
        foreach (var train in trains)
        {
            Console.WriteLine($"Train {train.TrainNumber} - {train.StartingStation} to {train.EndingStation}");

            foreach (var coach in train.Coaches)
            {
                Console.Write($"  {coach.Key}: ");

                if (coach.Value.Count > 0)
                {
                    Console.WriteLine(string.Join(", ", coach.Value));
                }
                else
                {
                    Console.WriteLine("No available seats");
                }
            }

            Console.WriteLine();
        }
    }


    public void Initialize()
    {
        string testData = "4\n" +
                          "37393 Ahmedabad-0 Pune-700\n" +
                          "37393 S1-72 S2-72 B1-72 A1-48 H1-24\n" +
                          "17726 Rajkot-0 Mumbai-750\n" +
                          "17726 S1-72 S2-72 B1-72 A1-48 H1-24\n" +
                          "22548 Ahmedabad-0 Surat-300\n" +
                          "22548 S1-15 S2-20 B1-36 B2-48\n" +
                          "72097 Mumbai-0 Jaipur-1000\n" +
                          "72097 S1-15 S2-20 B1-25 H1-10";

        string[] lines = testData.Split('\n');

        int numberOfTrains;
        if (!int.TryParse(lines[0], out numberOfTrains))
        {
            Console.WriteLine("Invalid number of trains format.");
            return;
        }

        for (int i = 1; i < lines.Length - 1; i += 2)
        {
            string[] trainInfo = lines[i].Split();
            if (trainInfo.Length != 3)
            {
                Console.WriteLine("Invalid train information format.");
                return;
            }

            int trainNumber;
            if (!int.TryParse(trainInfo[0], out trainNumber))
            {
                Console.WriteLine("Invalid train number format.");
                return;
            }

            string startingStation = trainInfo[1].Split('-')[0];
            string endingStation = trainInfo[2].Split('-')[0];

            int distance;
            if (!int.TryParse(trainInfo[2].Split('-')[1], out distance))
            {
                Console.WriteLine("Invalid distance format.");
                return;
            }

            Train train = new Train
            {
                TrainNumber = trainNumber,
                StartingStation = startingStation,
                EndingStation = endingStation,
                Distance = distance,
                Coaches = new Dictionary<string, List<int>>()
            };

            string[] coachInfo = lines[i + 1].Split();
            for (int j = 1; j < coachInfo.Length; j++)
            {
                string[] coachDetails = coachInfo[j].Split('-');
                if (coachDetails.Length != 2)
                {
                    Console.WriteLine("Invalid coach information format.");
                    return;
                }

                string coachType = coachDetails[0];
                int numberOfSeats;
                if (!int.TryParse(coachDetails[1], out numberOfSeats) || numberOfSeats < 1 || numberOfSeats > 72)
                {
                    Console.WriteLine("Invalid number of seats format or out of range (1-72).");
                    return;
                }

                train.Coaches.Add(coachType, Enumerable.Range(1, numberOfSeats).ToList());
            }

            trains.Add(train);
        }
    }

    public void BookTicket(string route, DateTime date, string coachType, int numberOfPassengers)
    {
        var availableTrains = SearchTrains(route, date);

        if (availableTrains.Any())
        {
            foreach (var train in availableTrains)
            {
                string uppercaseCoachType = coachType.ToUpper();

                // Check if there is any coach type starting with the provided coach type
                var matchingCoaches = train.Coaches.Keys
                    .Where(key => key.StartsWith(uppercaseCoachType))
                    .ToList();

                if (matchingCoaches.Any())
                {
                    // Use the first matching coach type
                    string actualCoachType = matchingCoaches.First();

                    if (train.Coaches[actualCoachType].Count >= numberOfPassengers)
                    {
                        int fare = CalculateFare(train, actualCoachType, numberOfPassengers);
                        int pnr = GeneratePNR();

                        // Update available seats
                        var bookedSeats = BookSeats(train.Coaches[actualCoachType], numberOfPassengers);
                        UpdateSeatAvailability(train, actualCoachType, bookedSeats);

                        // Display booking details
                        string coachTypeName = GetCoachTypeName(actualCoachType);
                        Console.WriteLine($"Booking Successful - PNR: {pnr}, Fare: {fare}, Coach Type: {coachTypeName}");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Not enough available seats in the specified coach type.");
                        return;
                    }
                }
            }
        }

        Console.WriteLine("No available trains or seats matching the criteria.");
    }

    private string GetCoachTypeName(string coachType)
    {
        switch (coachType)
        {
            case "S":
                return "Sleeper (SL)";
            case "B":
                return "3 Tier AC (3A)";
            case "A":
                return "2 Tier AC (2A)";
            case "H":
                return "1st class AC (1A)";
            default:
                return "Unknown Coach Type";
        }
    }

    private List<Train> SearchTrains(string route, DateTime date)
    {
        // Extracting starting and ending stations from the provided route
        string[] stations = route.Split();
        string startingStation = stations[0];
        string endingStation = stations[1];

        // Filter trains based on starting and ending stations
        var availableTrains = trains
            .Where(t => t.StartingStation == startingStation && t.EndingStation == endingStation)
            .ToList();

        // Filter out trains that have already departed (completed their journey) on the provided date
        availableTrains = availableTrains
            .Where(t => date.Date <= DateTime.Now.Date)
            .ToList();

        return availableTrains;
    }

    private int CalculateFare(Train train, string coachType, int numberOfPassengers)
    {
        int farePerKm;

        switch (coachType)
        {
            case "SL":
                farePerKm = 1;
                break;
            case "3A":
                farePerKm = 2;
                break;
            case "2A":
                farePerKm = 3;
                break;
            case "1A":
                farePerKm = 4;
                break;
            default:
                throw new ArgumentException("Invalid coach type.");
        }

        return train.Distance * farePerKm * numberOfPassengers;
    }

    private List<int> BookSeats(List<int> availableSeats, int numberOfPassengers)
    {
        // Book seats from lowest to highest
        return availableSeats.OrderBy(s => s).Take(numberOfPassengers).ToList();
    }

    private void UpdateSeatAvailability(Train train, string coachType, List<int> bookedSeats)
    {
        // Remove booked seats from available seats
        foreach (var seat in bookedSeats)
        {
            train.Coaches[coachType].Remove(seat);
        }
    }

    private int GeneratePNR()
    {
        return ++lastPNR;
    }
}

class Program
{
    static void Main()
    {
        TrainReservationSystem system = new TrainReservationSystem();
        system.Initialize();

        system.PrintAvailableTrains();

        Console.Write("Enter route, date, class, and number of passengers (e.g., Rajkot Mumbai 2023-03-15 SL 6): ");
        string[] input = Console.ReadLine().Split();

        string route = $"{input[0]} {input[1]}";
        DateTime date = DateTime.Parse(input[2]);
        string coachType = input[3];
        int numberOfPassengers = int.Parse(input[4]);

        system.BookTicket(route, date, coachType, numberOfPassengers);
    }
}