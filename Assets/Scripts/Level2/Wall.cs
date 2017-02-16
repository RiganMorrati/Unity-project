using UnityEngine;

public class Wall : MonoBehaviour
{
    protected readonly Vector3 startPos = new Vector3(-68.5f, 96.5f); //left top corner
    protected readonly Vector3 xStep = new Vector3(27.4f, 0.0f);
    protected readonly Vector3 yStep = new Vector3(0.0f, -27.4f);

    protected Point p;
    public Point P
    {
        get
        {
            return p;
        }
        set
        {
            p = value;
            transform.localPosition = startPos + xStep * value.x + yStep * value.y;
        }
    }
}