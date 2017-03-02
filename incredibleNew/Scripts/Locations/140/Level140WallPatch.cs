using UnityEngine;
using DG.Tweening;

public class Level140WallPatch : MonoBehaviour
{
    public Vector3 endPos = new Vector3(1.944f, -2.957f, -0.035f);
    public TutorialStar wallPatchStar, keyStar;
    public Entity keyEntity;

    const float fallTime = 0.3f;

    public bool IsOpened { get; private set; }
    public void Fall()
    {
        if (IsOpened)
            return;
        wallPatchStar.Complete();
        IsOpened = true;
        AudioManager.Instance.PlaySound("HitWood");
        transform.DOLocalMove(endPos, fallTime).OnComplete(()=>
        {
            keyStar.Show();
            keyEntity.enabled = true;
        });
    }
}