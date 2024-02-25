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