using UnityEngine;
using DG.Tweening;

public class Level140Lock : MonoBehaviour
{
    public LocationController controller;
    public Entity keyEntity;
    public TutorialStar starLock, starKey;
    public Level140Lock another;

    const float fallTime = 0.5f;

    public bool isEnabled = false;
    bool isLocked = true;
    void Start()
    {
        keyEntity.IsDestroy = false;
        keyEntity.onUse += () =>
        {
            if (!isEnabled)
                return;
            AudioManager.Instance.PlaySound("Unlock");
            keyEntity.gameObject.SetActive(false);
            transform.DOShakeScale(0.2f, 0.1f).OnComplete(() =>
            {
                transform.DOLocalMoveY(transform.localPosition.y - 0.4f, fallTime).
                SetDelay(0.4f).OnStart(()=>GetComponent<SpriteRenderer>().DOFade(0, fallTime - 0.05f));
                starLock.Complete();
                isLocked = false;
                if (!another.isLocked && !this.isLocked)
                    controller.CompleteLevel();
            });
        };
        keyEntity.onBeginDrag += starKey.Complete;
    }
}