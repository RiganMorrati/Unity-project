using UnityEngine;
using DG.Tweening;

public class Level141Cell : MonoBehaviour
{
    public Transform validItem;

    public Transform GetItem()
    {
        return transform.childCount > 0 ? transform.GetChild(0) : null;
    }

    public bool IsValidItem()
    {
        Transform item = GetItem();
        return item != null && item.Equals(validItem);
    }

    public void HideCell()
    {
        validItem.GetComponent<SpriteRenderer>().DOFade(0, 0.8f);
        enabled = false;
    }
}