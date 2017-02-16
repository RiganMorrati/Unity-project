using UnityEngine;
using UnityEngine.UI;

public class Level2 : MonoBehaviour
{
    public Button upButton;
    public Button downButton;
    public Button leftButton;
    public Button rightButton;
    public Labirinth labirinth;
    public Candy candy;
    public Key key;

    const float animateTime = 0.5f;

    void Start()
    {
        candy.onMoveEnded = ()=>
        {
            if (candy.P == labirinth.endPoint)
                KeyApearing();
        };
    }

    public void OnUpButtonClick()
    {
        candy.MoveTo(labirinth.GetWayPoint(candy.P, Dirrection.Up));
    }

    public void OnDownButtonClick()
    {
        candy.MoveTo(labirinth.GetWayPoint(candy.P, Dirrection.Down));
    }

    public void OnRightButtonClick()
    {
        candy.MoveTo(labirinth.GetWayPoint(candy.P, Dirrection.Right));
    }

    public void OnLeftButtonClick()
    {
        candy.MoveTo(labirinth.GetWayPoint(candy.P, Dirrection.Left));
    }

    void KeyApearing()
    {
        upButton.enabled = false;
        downButton.enabled = false;
        leftButton.enabled = false;
        rightButton.enabled = false;
        iTween.ScaleTo(candy.gameObject, iTween.Hash("scale", Vector3.zero, "time", animateTime, "easeType", iTween.EaseType.linear));
        key.Appear();
    }
}