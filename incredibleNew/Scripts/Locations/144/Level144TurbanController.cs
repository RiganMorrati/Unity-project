using UnityEngine;

public class Level144TurbanController : MonoBehaviour
{
    public Level144Turban[] turbans;

    public void SetMovable(bool isMovable)
    {
        for (int i = 0; i < turbans.Length; i++)
            turbans[i].SetMovable(isMovable);
    }
}