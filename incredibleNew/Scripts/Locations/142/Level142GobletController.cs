using UnityEngine;
using DG.Tweening;

public class Level142GobletController : MonoBehaviour
{
    public Level142Goblet[] goblets;
    public int keyState { get; set; }
    public LocationController controller;
    public Transform ball;

    private Animator shuffleGoblets;
    private int step;

    void Start()
    {
        shuffleGoblets = GetComponent<Animator>();
        shuffleGoblets.enabled = false;
        Invoke("CloseAllGoblets", 1);
    }

    public void Colliders(bool state)
    {
        for (int i = 0; i < goblets.Length; i++)
            goblets[i].GetComponent<Collider2D>().enabled = state;
    }

    public bool IsValidGoblet(Level142Goblet goblet)
    {
        return goblets[keyState].Equals(goblet);
    }

    public void ShuffleGoblets()
    {
        Colliders(false);
        keyState = Random.Range(0, 3);
        ball.parent = goblets[keyState].transform;
        ball.localPosition = new Vector3(0.0f, -0.37f, 0.01f);
        goblets[keyState].onOpenGoblet += () => ball.gameObject.SetActive(true); 
        goblets[keyState].onCloseGoblet += () => ball.gameObject.SetActive(false); 
        //Shuffle
        step = Random.Range(3, 7);
        shuffleGoblets.enabled = true;
    }

    public void EndMix()
    {
        if (--step == 0)
        {
            shuffleGoblets.enabled = false;
            Colliders(true);
        }
    }

    public void OpenGoblets(Level142Goblet g)
    {
        for (int i = 0; i < goblets.Length; i++)
        {
            if (!goblets[i].Equals(g))
                goblets[i].OpenGoblet();
            goblets[i].star.Complete();
        }
    }

    public void CloseAllGoblets()
    {
        for (int i = 0; i < goblets.Length; i++)
            goblets[i].CloseGoblet();
        ball.gameObject.SetActive(false);
        Invoke("ShuffleGoblets", 0.5f);
    }

    public void Valid()
    {
        transform.DOShakeScale(0.2f, 0.1f).SetDelay(1).OnStart(()=>AudioManager.Instance.PlaySound("Unlock")).OnComplete(() =>
        {
            for (int i = 0; i < goblets.Length; i++)
                goblets[i].HideGoblet();
            ball.GetComponent<SpriteRenderer>().DOFade(0, 0.8f);
            controller.CompleteLevel();
        });
    }
}