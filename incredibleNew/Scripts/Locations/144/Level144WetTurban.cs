using UnityEngine;

public class Level144WetTurban : MonoBehaviour
{
    public Level144TurbanController turbans;
    public Level144TurbanColors color;

    Entity entity = null;
	void Start ()
    {
        entity.IsDestroy = false;
        entity.onUse += () =>
        {
            Level144Ashe ashe = entity.usedObj.GetComponent<Level144Ashe>();
            if (ashe.IsStewed)
                return;
            AudioManager.Instance.PlaySound("Steam");
            Hide();
            turbans.SetMovable(true);
            transform.position = entity.startPosition;
            ashe.ThrowTurban(color);
        };
    }

    public void Show()
    {
        if(entity == null)
            entity = GetComponent<Entity>();
        entity.enabled = true;
        gameObject.SetActive(true);
    }

    void Hide()
    {
        entity.enabled = false;
        gameObject.SetActive(false);
    }
}