using UnityEngine;

public enum Level144TurbanColors : byte
{
    Black,
    Yellow,
    White
}

public class Level144Turban : MonoBehaviour
{
    public Level144TurbanColors color;
    public Level144TurbanController turbans;
    public Level144WetTurbansController controller;
    Entity entity;
    void Start()
    {
        entity = GetComponent<Entity>();
        entity.IsDestroy = false;
        entity.onUse += () =>
        {
            AudioManager.Instance.PlaySound("Water");
            turbans.SetMovable(false);
            transform.position = entity.startPosition;
            controller.Show(color);
        };
    }

    public void SetMovable(bool isMovable)
    {
        entity.enabled = isMovable;
    }
}