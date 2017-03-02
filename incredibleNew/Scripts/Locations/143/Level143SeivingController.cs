using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Level143SeivingController : ShakePhone
{
    public GameObject seive, seive2, sandBucket, seiveSand, sand;
    public event System.Action onSeived;

    bool isReadyForSeiving = false;
    const float pouringTime = 2.0f;
    const float shakeTime = 1.0f;
    const float flyTime = 0.3f;

    Vector3 startPos = new Vector3(1.27f, -1.7f, -0.01f);
    Vector3 endPos = new Vector3(-0.32f, 0.78f, -0.01f);
    const float endRotate = -89f;

    public override void Start()
    {
        onShakeComplete += StartSeiving;
        base.Start();
        active = false;
    }

    Level143Bucket bucket;
    public void StartPouring(Level143Bucket b)
    {
        bucket = b;
        isReadyForSeiving = false;
        StartCoroutine(PouringCoroutine());
    }

    IEnumerator PouringCoroutine()
    {
        MoveSieveUp();
        MoveBucketUp();
        sand.SetActive(true);
        sand.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        sand.GetComponent<SpriteRenderer>().DOFade(1.0f, pouringTime);
        sandBucket.transform.DOShakeScale(pouringTime, 0.05f);
        yield return new WaitForSeconds(pouringTime);
        isReadyForSeiving = true;
        MoveBucketDown();
        active = true;
        yield return null;
    }

    public void StartSeiving()
    {
        active = false;
        if(isReadyForSeiving)
            StartCoroutine(SeivingCorutine());
    }

    IEnumerator SeivingCorutine()
    {
        seive2.transform.DOShakePosition(shakeTime, 0.03f);
        seiveSand.SetActive(true);
        sand.GetComponent<SpriteRenderer>().DOFade(0.0f, shakeTime);
        yield return new WaitForSeconds(shakeTime);
        sand.SetActive(false);
        seiveSand.SetActive(false);
        if (bucket.gem != null)
        {
            bucket.gem.Show();
            AudioManager.Instance.PlaySound("Spell");
        }
        else
            EndSeiving();
        yield return new WaitForSeconds(0.5f);
        isReadyForSeiving = false;
        yield return null;
    }

    public void EndSeiving()
    {
        MoveSieveDown();
        if (onSeived != null)
            onSeived();
    }

    public void MoveBucketUp()
    {
        sandBucket.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        sandBucket.GetComponent<SpriteRenderer>().DOFade(1.0f, flyTime).SetDelay(flyTime);
        sandBucket.SetActive(true);
    }

    public void MoveBucketDown()
    {
        bucket.Show();
        sandBucket.SetActive(false);
    }

    public void MoveSieveUp()
    {
        seive.transform.DOMove(endPos, flyTime);
        seive.transform.DORotate(new Vector3(0.0f, 0.0f, endRotate), flyTime);
        seive.transform.DOScale(new Vector3(1.0f, 1.33f, 1.0f), flyTime).OnComplete(() =>
        {
            seive2.SetActive(true);
            seive.SetActive(false);
        });
    }

    public void MoveSieveDown()
    {
        seive2.SetActive(false);
        seive.SetActive(true);
        seive.transform.DOMove(startPos, flyTime);
        seive.transform.DORotate(Vector3.zero, flyTime);
        seive.transform.DOScale(Vector3.one, flyTime);
    }
}