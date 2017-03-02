using DG.Tweening;
using UnityEngine;

public class Level140Obstacle : MonoBehaviour
{
    public Transform leftNails, rightNails;
    public event System.Action onRemoved, onRemoving;
    public TutorialStar star;

    protected const float fadeTime = 0.5f;

    public bool isEnabled = false;
    bool isLeftPulled = false, isRightPulled = false;
    public void RemoveNails(Transform nails)
    {
        if (!isEnabled)
            return;
        if (!isLeftPulled && nails == leftNails)
        {
            AudioManager.Instance.PlaySound("Iron");
            leftNails.gameObject.SetActive(!(isLeftPulled = true));
            transform.DOShakeScale(0.2f, 0.1f);
        }
        if (!isRightPulled && nails == rightNails)
        {
            AudioManager.Instance.PlaySound("Iron");
            rightNails.gameObject.SetActive(!(isRightPulled = true));
            transform.DOShakeScale(0.2f, 0.1f);
        }
        if (isLeftPulled && isRightPulled)
        {
            if (onRemoving != null)
                onRemoving();
            GetComponent<SpriteRenderer>().DOFade(0, fadeTime).OnComplete(() =>
            {
                if (onRemoved != null)
                    onRemoved();
            });
        }
    }

    public bool IsRemoved
    {
        get
        {
            return isLeftPulled && isRightPulled;
        }
    }
}