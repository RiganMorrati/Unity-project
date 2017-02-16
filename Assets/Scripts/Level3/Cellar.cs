using UnityEngine;
using System.Collections;

public class Cellar : MonoBehaviour
{
    public Collider2D cellarCollider;
    public Door door;

    public const float requiredAngle = 540.0f;
    const float animationTime = 1.0f;
    readonly Vector2 endPos = new Vector2(-147.0f, 101.0f);

    bool isSettled = false;
    float passedAngle = 0.0f;

    public void OnCellarClick()
    {
        if(!isSettled)
            PlayCellarAnimation();
    }

    void PlayCellarAnimation()
    {
        iTween.MoveTo(gameObject, endPos, animationTime);
        iTween.RotateTo(gameObject, iTween.Hash("rotation", Vector3.zero, "time", animationTime, "oncomplete", "SellarSettled"));
    }

    void SellarSettled()
    {
        isSettled = isDragable = true;
    }

    bool isDragable = false;
    bool isDraging = false;
    RaycastHit2D[] hitted;
    Vector3 clickPos = Vector3.zero;
    Vector3 currentPos = Vector3.zero;
    bool isMouseButtonPressedInPrevFrame = false;
    float angle = -180.0f;

    void Update()
    {
        #region Draging started
        if (!isDragable)
            return;
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            clickPos = Input.touches[0].position;
#else
        if (Input.GetAxis("Fire1") > 0.5f && !isMouseButtonPressedInPrevFrame)
        {
            clickPos = Input.mousePosition;
#endif
            hitted = Physics2D.CircleCastAll(Camera.main.ScreenToWorldPoint(clickPos), 1.0f, Vector2.zero);
            if (hitted.Length > 0 && clickPos != Vector3.zero)
            {
                isDraging = false;
                foreach (RaycastHit2D hit in hitted)
                    if (hit.collider == cellarCollider)
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
        }
        #endregion
        #region Dragging
        if (isDraging)
        {
            if(passedAngle >= requiredAngle)
            {
                isDraging = false;
                isDragable = false;
                StartCoroutine(WinScenario());
            }
#if UNITY_ANDROID || UNITY_IOS
            currentPos = Input.touches[0].position;
#else
            currentPos = Input.mousePosition;
#endif
            Vector2 pos = Camera.main.ScreenToWorldPoint(currentPos);
            Vector2 c = (pos - endPos).normalized;
            float a = Mathf.Acos(Vector2.Dot(c, Vector2.up)) * Mathf.Rad2Deg * (c.x > 0 ? -1 : 1);
            transform.Rotate(0.0f, 0.0f, a - angle);
            door.SetState(passedAngle += (Mathf.Abs(a - angle) < 90 ? a - angle : 0.0f));
            angle = a;
        }
        #endregion
    }

    IEnumerator WinScenario()
    {
        yield return new WaitForSeconds(animationTime);
        LevelManager.lManager.winPanel.SetActive(true);
        yield return null;
    }
}