using UnityEngine;

public enum Dirrection : byte { Left, Up, Right, Down }

public class Labirinth : MonoBehaviour
{
    public GameObject wall;

    readonly bool[,] arr = new bool[6, 8]
        {
            { true, true, false, false, false, false, true, false },
            { false, false, false, true, true, false, true, false },
            { false, true, false, false, true, false, false, true },
            { true, true, true, false, false, true, false, false },
            { false, false, false, false, false, true, false, false },
            { false, true, false, true, true, false, true, false },
        };
    public readonly Point endPoint = new Point(5, 7);

	void Start ()
    {
        for (sbyte x = 0; x < arr.GetLength(0); x++)
            for (sbyte y = 0; y < arr.GetLength(1); y++)
            {
                if (arr[x, y])
                {
                    GameObject wObject = Instantiate(wall);
                    wObject.transform.SetParent(transform);
                    wObject.transform.name = "Wall_" + x + ":" + y;
                    Wall w = wObject.GetComponent<Wall>();
                    w.P = new Point(x, y);
                }
            }
	}

    public Point GetWayPoint(Point from, Dirrection dir)
    {
        Point res = null;
        switch (dir)
        {
            case Dirrection.Left:
                for (sbyte i = from.x; i >= 0 && !arr[i, from.y]; i--)
                    res = new Point(i, from.y);
                break;
            case Dirrection.Up:
                for (sbyte i = from.y; i >= 0 && !arr[from.x, i]; i--)
                    res = new Point(from.x, i);
                break;
            case Dirrection.Right:
                for (sbyte i = from.x; i < arr.GetLength(0) && !arr[i, from.y]; i++)
                    res = new Point(i, from.y);
                break;
            case Dirrection.Down:
                for (sbyte i = from.y; i < arr.GetLength(1) && !arr[from.x, i]; i++)
                    res = new Point(from.x, i);
                break;
        }
        return res;
    }
}