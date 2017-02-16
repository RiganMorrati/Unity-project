using System.Collections.Generic;
using UnityEngine;

public enum Angle : byte { A0, A60, A120, A180, A240, A300 }

public class Circle : MonoBehaviour
{
    public List<Number> numbers;
    public event System.Action onMoveEnded;
    bool isMoving = false;

    const float moveTime = 0.5f;
    public static readonly byte nCount = (byte)System.Enum.GetNames(typeof(Angle)).Length;
    readonly float angleStep = 360.0f / System.Enum.GetNames(typeof(Angle)).Length;

    public Number this[Angle a]
    {
        get
        {
            int i = (nCount + (int)a - (int)angle) % numbers.Count;
            return numbers[i];
        }
    }

    Angle a = Angle.A0;
    public Angle angle
    {
        set
        {
            if (!isMoving)
            {
                isMoving = true;
                a = (Angle)((byte)value % nCount);
                Vector3 r = new Vector3(0.0f, 0.0f, -angleStep * (byte)a);
                iTween.RotateTo(gameObject, iTween.Hash("rotation", r, "islocal", true, "time", moveTime, "oncomplete", "Stoped"));
            }
        }
        get
        {
            return a;
        }
    }

    void Stoped()
    {
        isMoving = false;
        if (onMoveEnded != null)
            onMoveEnded();
    }
}