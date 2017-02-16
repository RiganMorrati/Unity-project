using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1 : MonoBehaviour
{
    public List<Circle> circles;
    public Select select;
    public Button rotateButton;
    public Button moveButton;

    const byte requiredSum = 15;
    const float winDelay = 0.5f;

    void Start()
    {
        foreach (Circle circle in circles)
            circle.onMoveEnded += Count;
        select.onMoveEnded += Count;
    }

    public void OnRotateButtonClick()
    {
        circles[select.Position].angle++;
    }

    public void OnMoveButtonClick()
    {
        select.Position ++;
    }

    void Count()
    {
        byte count = 0;
        foreach (Angle angle in System.Enum.GetValues(typeof(Angle)))
        {
            byte sum = 0;
            foreach (Circle circle in circles)
                sum += circle[angle].Value;
            foreach (Circle circle in circles)
                circle[angle].SetColor(sum == requiredSum ? Color.green : Color.red);
            if (sum == requiredSum)
                count++;
        }
        if (count == Circle.nCount)
            StartCoroutine(WinScenario());
    }

    IEnumerator WinScenario()
    {
        rotateButton.enabled = false;
        moveButton.enabled = false;
        yield return new WaitForSeconds(winDelay);
        LevelManager.lManager.winPanel.SetActive(true);
        yield return null;
    }
}