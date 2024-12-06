

char[,] array = ReadFile("D:\\MP\\Guard\\Guard\\input.txt");
var guardIdx = SearchIn2DArray(array, '^');
Guard guard = new(guardIdx.Item1, guardIdx.Item2);

    guard.Move(array);
Console.WriteLine(guard.uniqPositions.Count);


char[,] ReadFile(string path)
{
    string[] lines = File.ReadAllLines(path);

    int height = lines[0].Length;
    int width = lines.Length;

    char[,] charArray = new char[height, width];

    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            charArray[i, j] = lines[i][j]; 
        }
    }

    return charArray;
}
 (int,int) SearchIn2DArray(char[,] array, char target)
{
    for (int i = 0; i < array.GetLength(0); i++) 
    {
        for (int j = 0; j < array.GetLength(1); j++) 
        {
            if (array[i, j] == target) 
            {
                return (i,j); 
            }
        }
    }
    return (-1,-1);
}

public class Guard
{
    public HashSet<Point> uniqPositions = new HashSet<Point>();
    Point[] directions =
    {
        new (-1,0),
        new (0,1),
        new (1,0),
        new (0,-1),
    };
    int currDirectionIdx;
    Point Direction { get => directions[currDirectionIdx]; }
    Point Position;
    public Guard(int x, int y)
    {
        Position = new Point(x, y);
        currDirectionIdx = 0;
    }
    public void Move(char[,] array)
    {

        while (true)
        {
            uniqPositions.Add(Position);
            Point newPos = Position + Direction;
            if (OutOfBounds(array, newPos)) break;
            if (array[newPos.X, newPos.Y] == '#')
            {
                ChangeDirection();
            }
            else
            {
                Position = newPos;

            };
        }
    }
    public void ChangeDirection()
    {
        if (currDirectionIdx == directions.Length - 1) currDirectionIdx = 0;
        else currDirectionIdx++;
    }

    private bool OutOfBounds(char[,] array, Point newPos)
    {
        int xBound = array.GetLength(0); int yBound = array.GetLength(1);
        return (
            newPos.X < 0 || newPos.Y < 0 ||
            newPos.X >= xBound ||
            newPos.Y >= yBound
            );
    }
}

public record struct Point(int X, int Y)
{
    public static Point operator +(Point p1, Point p2)
    {
        return new Point(p1.X + p2.X, p1.Y + p2.Y);
    }
}

