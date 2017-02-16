using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public Image image;
    public Collider2D keyCollider;
    public Collider2D lockCollider;
    public Lock @lock;

    readonly Vector3 startPos = new Vector3(-89.0f, -263.0f, 0.0f);
    bool isDragable = false;

    public void Appear()
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", 0.0f, "to", 1.0f, "onupdate", "SetOpacity", "oncomplete", "Appeared"));
    }

    void SetOpacity(float a)
    {
        Color c = image.color;
        c.a = a;
        image.color = c;
    }

    void Appeared()
    {
        isDragable = true;
    }

    bool isDraging = false;
    RaycastHit2D[] hitted;
    Vector3 clickPos = Vector3.zero;
    Vector3 currentPos = Vector3.zero;
    bool isMouseButtonPressedInPrevFrame = false;

    void Update()
    {
        #region Draging started
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            clickPos = Input.touches[0].position;
#else
        if (isDragable && Input.GetAxis("Fire1") > 0.5f && !isMouseButtonPressedInPrevFrame)
        {
            clickPos = Input.mousePosition;
#endif
            hitted = Physics2D.CircleCastAll(Camera.main.ScreenToWorldPoint(clickPos), 1.0f, Vector2.zero);
            if (hitted.Length > 0 && clickPos != Vector3.zero)
            {
                isDraging = false;
                foreach (RaycastHit2D hit in hitted)
                    if (hit.collider == keyCollider)
                    {
                        isDraging = true;
                        break;
                    }
                if (!isDraging)
                    return;
                isMouseButtonPressedInPrevFrame = true;
            }
        }
        #endregion
        #region Draging stopped
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touches.Length == 0)
            isDraging = false;
#else
        if (Input.GetAxis("Fire1") < .5f)
            isDraging = false;
#endif
        if (!isDraging && isMouseButtonPressedInPrevFrame)
        {
            isMouseButtonPressedInPrevFrame = false;
            clickPos = Vector3.zero;
            hitted = Physics2D.CircleCastAll(Camera.main.ScreenToWorldPoint(currentPos), 1.0f, Vector2.zero);
            if (hitted.Length > 0)
            {
                foreach (RaycastHit2D hit in hitted)
                    if (hit.collider == lockCollider)
                    {
                        PlayHideAnimation();
                        return;
                    }
            }
            transform.localPosition = startPos;
        }
        #endregion
        #region Dragging
        if (isDraging)
        {
#if UNITY_ANDROID || UNITY_IOS
            currentPos = Input.touches[0].position;
#else
            currentPos = Input.mousePosition;
#endif
            Vector2 pos = Camera.main.ScreenToWorldPoint(currentPos);
            transform.position = pos;
        }
        #endregion
    }

    void PlayHideAnimation()
    {
        @lock.PlayUnlockanimation();
        image.color = Color.clear;
    }
}