using UnityEngine;

public class Level143Gem : MonoBehaviour
{
    public event System.Action onDeploy;
    public Entity entity;
    void Start()
    {
        entity.IsDestroy = false;
        IsDeployed = false;
        entity.onUse += () =>
        {
            entity.startPosition = entity.obj.transform.position + new Vector3(0.0f, 0.0f, -0.01f);
            AudioManager.Instance.PlaySound("Spell");
            entity.enabled = false;
            IsDeployed = true;
            if (onDeploy != null)
                onDeploy();
        };
    }
    public void Show()
    {
        gameObject.SetActive(true);
        entity.enabled = true;
    }
    public bool IsDeployed { get; private set; }
}