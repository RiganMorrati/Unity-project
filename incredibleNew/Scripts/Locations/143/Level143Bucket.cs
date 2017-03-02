using UnityEngine;

public class Level143Bucket : MonoBehaviour
{
    public Level143Gem gem = null;
    public event System.Action<Level143Bucket> onDrop;

    GameObject sand;
    Entity entity;
    Vector3 startPos;

    void Start()
    {
        entity = GetComponent<Entity>();
        startPos = transform.localPosition;
        sand = transform.FindChild("Sand").gameObject;
        entity.IsDestroy = false;
        IsEmpty = false;
        entity.onUse += Hide;
    }

    public void Hide()
    {
        AudioManager.Instance.PlaySound("Tap");
        transform.localPosition = startPos;
        GetComponent<Collider2D>().enabled = false;
        IsEmpty = true;
        entity.enabled = false;
        sand.SetActive(false);
        gameObject.SetActive(false);
        if (onDrop != null)
            onDrop(this);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void SetMovable(bool isMovable)
    {
        if (!IsEmpty)
            entity.enabled = isMovable;
    }

    public bool IsEmpty { get; private set; }
}