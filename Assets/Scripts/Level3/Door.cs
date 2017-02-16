using UnityEngine;

public class Door : MonoBehaviour
{
    const float startY = 120.0f;
    const float endY = 450.0f;

    public void SetState(float angle)
    {
        Vector2 pos = transform.localPosition;
        pos.y = Mathf.Clamp(startY + angle * (endY - startY) / Cellar.requiredAngle, startY, endY);
        transform.localPosition = pos;
    }
}