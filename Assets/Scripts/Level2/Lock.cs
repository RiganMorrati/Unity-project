using UnityEngine;
using UnityEngine.UI;

public class Lock : MonoBehaviour
{
    public Image image;

    const float animateTime = 0.5f;

    public void PlayUnlockanimation()
    {
        iTween.PunchScale(gameObject, new Vector3(0.1f, 0.1f, 0.0f), animateTime);
        iTween.ValueTo(gameObject, iTween.Hash("from", 1.0f, "to", 0.0f, "time", animateTime, "delay", animateTime / 2.0f,
            "onupdate", "SetOpacity", "oncomplete", "Unlocked"));
    }

    void SetOpacity(float a)
    {
        Color c = image.color;
        c.a = a;
        image.color = c;
    }

    void Unlocked()
    {
        LevelManager.lManager.winPanel.SetActive(true);
    }
}