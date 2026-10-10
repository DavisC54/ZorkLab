using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                // Show which room the player is in at the start of every turn
                Console.WriteLine(Rooms[Location.Row, Location.Column]);
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString;
                switch (command)
                {
                    case Commands.QUIT:
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.LOOK:
                        outputString = "This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;

                    // Try to move. Move() says whether it worked and then gives the result
                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command))
                        {
                            outputString = $"You moved {command}.";
                        }
                        else
                        {
                            outputString = "The way is shut!";
                        }
                        break;

                    default:
                        outputString = "Unknown command.";
                        break;
                }

                Console.WriteLine(outputString);
            }
        }

        private static Commands ToCommand(string commandString) => (Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKNOWN);

        // Moves the player one room in the given direction if there is a room there and returns true
        private static bool Move(Commands command)
        {
            // Move should only ever get a direction, nothin else
            if (command != Commands.NORTH && command != Commands.SOUTH && command != Commands.EAST && command != Commands.WEST)
            {
                throw new ArgumentException("Move only takes a direction.", nameof(command));
            }

            bool didMove = false;
            switch (command)
            {
                // North goes up a row and stops at the top row
                case Commands.NORTH:
                    if (Location.Row < Rooms.GetLength(0) - 1)
                    {
                        Location.Row++;
                        didMove = true;
                    }
                    break;

                // South goes down a row and stops at 0
                case Commands.SOUTH:
                    if (Location.Row > 0)
                    {
                        Location.Row--;
                        didMove = true;
                    }
                    break;

                // East goes right and stops at the last column
                case Commands.EAST:
                    if (Location.Column < Rooms.GetLength(1) - 1)
                    {
                        Location.Column++;
                        didMove = true;
                    }
                    break;

                // West goes left and stops at 0
                case Commands.WEST:
                    if (Location.Column > 0)
                    {
                        Location.Column--;
                        didMove = true;
                    }
                    break;
            }

            return didMove;
        }
        private static readonly string[,] Rooms = {
            { "Rocky Trail", "South of House", "Canyon View" },
            { "Forest", "West of House", "Behind House" },
            { "Dense Woods", "North of House", "Clearing" }
        };

        // Which spot in rooms the player is standing on
        private static (int Row, int Column) Location = (1, 1);
    }
}