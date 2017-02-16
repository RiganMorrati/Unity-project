public class Point
{
    public sbyte x;
    public sbyte y;

    public Point(sbyte x, sbyte y)
    {
        this.x = x;
        this.y = y;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        Point p = obj as Point;
        if (p == null)
            return false;
        return p.x == x && p.y == y;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static bool operator ==(Point a, Point b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Point a, Point b)
    {
        return !a.Equals(b);
    }
}