using UnityEngine;

public class Bush : MonoBehaviour
{
    public Collider2D bushCollider;

    const float leftPos = 170.0f;
    const float rightPos = 240.0f;
    const float y = -141.0f;
    bool isDragable = true;

    bool isDraging = false;
    RaycastHit2D[] hitted;
    Vector2 clickPos = Vector2.zero;
    Vector2 currentPos = Vector2.zero;
    Vector2 dPos = Vector2.zero;
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
            if (hitted.Length > 0 && clickPos != Vector2.zero)
            {
                isDraging = false;
                foreach (RaycastHit2D hit in hitted)
                    if (hit.collider == bushCollider)
                    {
                        isDraging = true;
                        break;
                    }
                if (!isDraging)
                    return;
                isMouseButtonPressedInPrevFrame = true;
                dPos = -Camera.main.ScreenToWorldPoint(clickPos);
                dPos += new Vector2(leftPos, y);
                dPos.y = 0.0f;
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
            if (transform.position.x == rightPos)
                isDragable = false;
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
            pos -= dPos;
            transform.localPosition = new Vector2(Mathf.Clamp(pos.x, leftPos, rightPos), y);
        }
        #endregion
    }
}