using UnityEngine;
using System.Collections;
//using DG.Tweening;

public enum Level144FirePower : byte
{
    None,
    Ashe,
    WeakFlame,
    Flame
}

public class Level144Ashe : MonoBehaviour
{
    public GameObject steam, weakFlame, flame, blackTurban, yellowTurban, whiteTurban;
    public Level144FirePower startfire;
    public event System.Action onStewing;

    const float time = 10.0f;

    Level144FirePower fire;
    GameObject turban = null;
    Coroutine turbanTimerCoroutine = null;
    //Sequence mySequence;
    void Start ()
    {
        SetFirePower(fire = startfire);
        //mySequence = DOTween.Sequence();
    }

    void SetFirePower(Level144FirePower fire)
    {
        this.fire = fire;
        IsStewed = false;
        weakFlame.SetActive(false);
        flame.SetActive(false);
        steam.SetActive(false);
        switch (fire)
        {
            case Level144FirePower.None:
                IsStewed = true;
                steam.SetActive(true);
                break;
            case Level144FirePower.WeakFlame:
                weakFlame.SetActive(true);
                break;
            case Level144FirePower.Flame:
                flame.SetActive(true);
                break;
        }
    }
    
    void RunTurbanTimer()
    {
        if (turbanTimerCoroutine != null)
            StopCoroutine(turbanTimerCoroutine);
        //mySequence.Kill();
        if (fire != Level144FirePower.None)
            turbanTimerCoroutine = StartCoroutine(TurbanTimer());
            //mySequence.Append(turban.GetComponent<SpriteRenderer>().DOFade(0.0f, time).OnComplete(()=>
            //{
            //    AudioManager.Instance.PlaySound("Wind");
            //    turban.GetComponent<SpriteRenderer>().color = Color.white;
            //    turban.SetActive(false);
            //    SetFirePower((int)fire < (int)startfire ? (Level144FirePower)((int)fire + 1) : fire);
            //    if (fire != startfire)
            //        RunTurbanTimer();
            //}));
    }

    IEnumerator TurbanTimer()
    {
        for (int i = 1; i <= 50; i++)
        {
            turban.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f - i / 50.0f);
            yield return new WaitForSeconds(time / 50.0f);
        }
        AudioManager.Instance.PlaySound("Wind");
        turban.GetComponent<SpriteRenderer>().color = Color.white;
        turban.SetActive(false);
        SetFirePower((int)fire < (int)startfire ? (Level144FirePower)((int)fire + 1) : fire);
        if (fire != startfire)
            RunTurbanTimer();
        turbanTimerCoroutine = null;
        yield return null;
    }

    public void ThrowTurban(Level144TurbanColors color)
    {
        whiteTurban.SetActive(false);
        yellowTurban.SetActive(false);
        blackTurban.SetActive(false);
        switch (color)
        {
            case Level144TurbanColors.Black:
                (turban = blackTurban).SetActive(true);
                blackTurban.GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case Level144TurbanColors.Yellow:
                (turban = yellowTurban).SetActive(true);
                yellowTurban.GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case Level144TurbanColors.White:
                (turban = whiteTurban).SetActive(true);
                whiteTurban.GetComponent<SpriteRenderer>().color = Color.white;
                break;
        }
        StewFlame();
    }

    void StewFlame()
    {
        fire = (Level144FirePower)((int)fire - 1);
        SetFirePower(fire);
        RunTurbanTimer();
        if (onStewing != null)
            onStewing();
    }

    public bool IsStewed { get; private set; }
}