using UnityEngine;

public class WinPanel : MonoBehaviour
{
    public void OnWinButtonClick()
    {
        LevelManager.lManager.CurrentLevel++;
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}