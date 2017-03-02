using DG.Tweening;
using UnityEngine;

public class Level140Hamer : MonoBehaviour
{
    public Entity entity;
    public Level140WallPatch wallPatch, stonePatch;

    const float fallTime = 0.7f;

    private Vector3 startPos = new Vector3(1.05f, -2.42f, -0.04f);
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
            if (entity.usedObj == wallPatch.transform)
                wallPatch.Fall();
            if (entity.usedObj == stonePatch.transform)
                stonePatch.Fall();
            if (wallPatch.IsOpened && stonePatch.IsOpened)
                GetComponent<SpriteRenderer>().DOFade(0.0f, 0.3f);
        };
    }
}