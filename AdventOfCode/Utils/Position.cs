namespace AdventOfCode.Utils;

public class Position
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
    public void UpdatePosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void IncrementPosition((int, int) direction)
    {
        X += direction.Item2;
        Y += direction.Item1;
    }
}
