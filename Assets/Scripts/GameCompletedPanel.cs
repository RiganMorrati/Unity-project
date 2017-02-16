using UnityEngine;

public class GameCompletedPanel : MonoBehaviour
{
	void Update ()
    {
#if UNITY_IOS
        if (Input.GetKeyDown(KeyCode.Home))
#else
        if (Input.GetKeyDown(KeyCode.Escape))
#endif
            TheEnd();
    }

    public void TheEnd()
    {
        Application.Quit();
    }
}