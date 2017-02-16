using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> lvls;
    public Transform canvas;
    public WinPanel winPanel;
    public GameObject gameCompletedPanel;

    public static LevelManager lManager;
    void Start()
    {
        lManager = this;
        CurrentLevel = 0;
    }

    GameObject currentLevel = null;
    sbyte currentLevelNumber = -1;
    public sbyte CurrentLevel
    {
        get
        {
            return currentLevelNumber;
        }
        set
        {
            if(value < 0 || value > 3 || value == currentLevelNumber)
                return;
            winPanel.SetActive(false);
            if (currentLevel != null)
                Destroy(currentLevel);
            if (value == 3)
            {
                WinScenario();
                return;
            }
            currentLevel = Instantiate(lvls[currentLevelNumber = value]);
            currentLevel.transform.SetParent(canvas);
            currentLevel.transform.SetAsFirstSibling();
            currentLevel.transform.name = "Level" + (currentLevelNumber + 1);
        }
    }

    void WinScenario()
    {
        gameCompletedPanel.SetActive(true);
    }
}