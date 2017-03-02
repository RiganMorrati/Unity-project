using System.Collections;
using UnityEngine;

public class Level141PuzzleController : MonoBehaviour
{
    public Level141Button[] buttons;
    public Level141Cell[] cells;
    public LocationController controller;

    public bool IsComplete { get; private set; }

    private byte[] startShake = { 0, 4, 7, 2, 1, 0, 6, 1, 3, 4, 7, 5, 0, 1 };

    void Start()
    {
        StartCoroutine(ShakeField());
    }

    public IEnumerator ShakeField()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < startShake.Length; i++)
        {
            buttons[startShake[i]].ChangeItems();
            yield return new WaitForSeconds(0.2f);
        }
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].GetComponent<Collider2D>().enabled = true;
        yield break;
    }

    public void CheckCells()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            if (!cells[i].IsValidItem()) break;
            if (i == cells.Length - 1)
            {
                IsComplete = true;
                Invoke("Complete", 0.3f);
            }
        }
    }

    private void Complete()
    {
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].HideButton();
        for (int i = 0; i < cells.Length; i++)
            cells[i].HideCell();
        controller.CompleteLevel();
    }
}