using UnityEngine;

public class Level144AsheController : MonoBehaviour
{
    public Level144Ashe[] ashes;
    public LocationController controller;

	void Start ()
    {
        for (int i = 0; i < ashes.Length; i++)
            ashes[i].onStewing += () =>
            {
                if (IsAllFlamesStewed())
                    controller.CompleteLevel();
            };
    }

    bool IsAllFlamesStewed()
    {
        for (int i = 0; i < ashes.Length; i++)
            if (!ashes[i].IsStewed)
                return false;
        return true;
    }
}