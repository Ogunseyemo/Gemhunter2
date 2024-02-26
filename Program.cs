using System;

class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

class Player
{
    public string Name { get; }
    public Position Position { get; set; }
    public int GemCount { get; set; }
    public int Turns { get; set; }

    public Player(string name, Position position)
    {
        Name = name;
        Position = position;
        GemCount = 0;
        Turns = 0;
    }

    public void Move(char direction)
    {
        Turns++;
        switch (direction)
        {
            case 'U':
                if (Position.Y > 0) Position.Y--;
                break;
            case 'D':
                if (Position.Y < 5) Position.Y++;
                break;
            case 'L':
                if (Position.X > 0) Position.X--;
                break;
            case 'R':
                if (Position.X < 5) Position.X++;
                break;
        }
    }
}
class Cell
{
    public string Occupant { get; set; }

    public Cell(string occupant)
    {
        Occupant = occupant;
    }
}
class Board
{
    public Cell[,] Grid { get; }

    public Board()
    {
        Grid = new Cell[6, 6];
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        // Initialize cells with empty spaces
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Grid[i, j] = new Cell("-");
            }
        }

        // Place players
        Grid[0, 0].Occupant = "P1";
        Grid[5, 5].Occupant = "P2";

        // Place gems (random positions)
        Random random = new Random();
        for (int i = 0; i < 5; i++)
        {
            int gemX = random.Next(6);
            int gemY = random.Next(6);
            Grid[gemX, gemY].Occupant = "G";
        }

        // Place obstacles (random positions)
        for (int i = 0; i < 8; i++)
        {
            int obstacleX = random.Next(6);
            int obstacleY = random.Next(6);
            // Make sure not to overwrite players or gems
            if (Grid[obstacleX, obstacleY].Occupant == "-")
            {
                Grid[obstacleX, obstacleY].Occupant = "O";
            }
            else
            {
                i--;
            }
        }
    }

    public void Display()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Console.Write($"{Grid[i, j].Occupant} ");
            }
            Console.WriteLine();
        }
    }

    public bool IsValidMove(Player player, char direction)
    {
        int newX = player.Position.X;
        int newY = player.Position.Y;

        switch (direction)
        {
            case 'U':
                newY--;
                break;
            case 'D':
                newY++;
                break;
            case 'L':
                newX--;
                break;
            case 'R':
                newX++;
                break;
        }

        // Check if the new position is within bounds and not an obstacle
        return newX >= 0 && newX < 6 && newY >= 0 && newY < 6 && Grid[newY, newX].Occupant != "O";
    }

    public void UpdatePlayerPosition(Player player, Position previousPosition)
    {
        Grid[previousPosition.Y, previousPosition.X].Occupant = "-";
        Grid[player.Position.Y, player.Position.X].Occupant = player.Name;
    }

    public void CollectGem(Player player)
    {
        if (Grid[player.Position.Y, player.Position.X].Occupant == "G")
        {
            player.GemCount++;
            Grid[player.Position.Y, player.Position.X].Occupant = "-";
            Console.WriteLine($"{player.Name} found a gem!");
        }
    }
}
class Game
{
    public Board Board { get; }
    public Player Player1 { get; }
    public Player Player2 { get; }
    public int TotalTurns { get; private set; }

    public Game()
    {
        Board = new Board();
        Player1 = new Player("P1", new Position(0, 0));
        Player2 = new Player("P2", new Position(5, 5));
        TotalTurns = 0;
    }
}