using UnityEngine;

public class Select : MonoBehaviour
{
    public event System.Action onMoveEnded;
    bool isMoving = false;
    const byte ringCount = 3;
    const float moveTime = 0.5f;

    Vector3 startPos = new Vector3(14.0f, 147.0f, 0.0f);
    Vector3 step = new Vector3(0.0f, -50.0f, 0.0f);

    byte p = 0;
    public byte Position
    {
        get
        {
            return p;
        }
        set
        {
            if (!isMoving)
            {
                isMoving = true;
                if ((p = value) >= ringCount )
                    p = 0;
                iTween.MoveTo(gameObject, iTween.Hash("position", startPos + step * p, "islocal", true, "time", moveTime, "oncomplete", "Stoped"));
            }
        }
    }

    void Stoped()
    {
        isMoving = false;
        if (onMoveEnded != null)
            onMoveEnded();
    }
}