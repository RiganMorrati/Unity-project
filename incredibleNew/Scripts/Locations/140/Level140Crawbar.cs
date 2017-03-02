using DG.Tweening;
using UnityEngine;

public class Level140Crawbar : MonoBehaviour
{
    public Entity entity;
    public Level140Obstacle chain, topPlank, bottomPlank;

    const float fallTime = 0.7f;

    private Vector3 startPos = new Vector3(-0.58f, -2.02f, -0.04f);
    public void MoveToStartPosition()
    {
        transform.DOLocalJump(startPos, 1, 1, fallTime).OnComplete(() =>
        {
            entity.enabled = true;
            AudioManager.Instance.PlaySound("HitWood");
        });
    }

    void Start()
    {
        entity.IsDestroy = false;
        entity.onUse += () =>
        {
            if (entity.usedObj.parent == chain.transform)
                chain.RemoveNails(entity.usedObj);
            if (entity.usedObj.parent == topPlank.transform)
                topPlank.RemoveNails(entity.usedObj);
            if (entity.usedObj.parent == bottomPlank.transform)
                bottomPlank.RemoveNails(entity.usedObj);
            if (chain.IsRemoved && topPlank.IsRemoved && bottomPlank.IsRemoved)
                GetComponent<SpriteRenderer>().DOFade(0.0f, 0.3f);
        };
    }
}