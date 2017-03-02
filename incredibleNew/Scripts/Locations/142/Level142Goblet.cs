using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Level142Goblet : MonoBehaviour, IPointerClickHandler
{
    public Level142GobletController gobletController;
    public TutorialStar star;
    public event System.Action onOpenGoblet, onCloseGoblet;

    void Start()
    {
        star.Show();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gobletController.Colliders(false);
        transform.DOShakeScale(0.2f, 0.1f).OnComplete(() => {
            OpenGoblet();
            Invoke("OpenGoblets", 0.5f);
        });
    }

    private void OpenGoblets()
    {
        gobletController.OpenGoblets(this);
        if (gobletController.IsValidGoblet(this))
            gobletController.Valid();
        else
            Invoke("CloseGoblets", 1);
    }

    private void CloseGoblets()
    {
        if (onCloseGoblet == null)
            gobletController.CloseAllGoblets();
    }

    public void OpenGoblet()
    {
        AudioManager.Instance.PlaySound("Tap");
        GetComponent<SpriteRenderer>().DOFade(0.5f, 0.3f).OnComplete(() =>
        {
            if (onOpenGoblet != null)
            {
                onOpenGoblet();
                onOpenGoblet = null;
            }
        });
    }

    public void CloseGoblet()
    {
        GetComponent<SpriteRenderer>().DOFade(1.0f, 0.3f).OnComplete(() =>
        {
            if (onCloseGoblet != null)
            {
                onCloseGoblet();
                onCloseGoblet = null;
            }
        });
    }

    public void HideGoblet()
    {
        GetComponent<SpriteRenderer>().DOFade(0, 0.8f);
    }
}