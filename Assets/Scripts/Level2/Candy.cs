using UnityEngine;

public class Candy : Wall
{
    public System.Action onMoveEnded;
    bool isMoving = false;
    const float moveTime = 0.2f;

    public Candy()
    {
        p = new Point(5, 0);
    }

    public void MoveTo(Point pos)
    {
        if (pos == null || isMoving)
            return;
        byte wayLength = 0;
        Vector3 movePos = startPos + xStep * pos.x + yStep * pos.y;
        if (pos.x != p.x)
            wayLength = (byte)System.Math.Abs(pos.x - p.x);
        if (pos.y != p.y)
            wayLength = (byte)System.Math.Abs(pos.y - p.y);
        isMoving = true;
        iTween.MoveTo(gameObject, iTween.Hash("position", movePos, "islocal", true, "time", moveTime * wayLength, 
            "oncomplete", "Stoped", "easeType", iTween.EaseType.linear));
        p = pos;
    }

    void Stoped()
    {
        isMoving = false;
        if (onMoveEnded != null)
            onMoveEnded();
    }
}