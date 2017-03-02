using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Level141Button : MonoBehaviour, IPointerClickHandler
{
    public Level141Cell[] cells = new Level141Cell[4];
    public Level141PuzzleController controller;
    public TutorialStar star;

    private SpriteRenderer rend;
    private new Collider2D collider;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        star.Show();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!controller.IsComplete)
        {
            AudioManager.Instance.PlaySound("Tap");
            ChangeItems();
            controller.CheckCells();
        }
    }

    public void ChangeItems()
    {
        cells[0].GetItem().SetParent(cells[1].transform);
        cells[1].GetItem().SetParent(cells[2].transform);
        cells[2].GetItem().SetParent(cells[3].transform);
        cells[3].GetItem().SetParent(cells[0].transform);

        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].GetItem().DOKill(true);
            cells[i].GetItem().DOLocalMove(Vector3.zero, 0.2f);
        }
    }

    public void HideButton()
    {
        rend.DOFade(0, 0.8f);
        star.Complete();
        collider.enabled = enabled = false;
    }
}