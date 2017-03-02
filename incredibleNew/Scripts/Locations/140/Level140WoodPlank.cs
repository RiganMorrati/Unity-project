using DG.Tweening;

public class Level140WoodPlank : Level140Obstacle
{
    public Level140WoodPlank anotherPlank;
    public Level140Lock redLock, blueLock;

    void Start()
    {
        onRemoving += star.Complete;
        onRemoved += () =>
        {
            star.Complete();
            if(this.IsRemoved && anotherPlank.IsRemoved)
            {
                redLock.isEnabled = true;
                blueLock.isEnabled = true;
                redLock.starLock.Show();
                blueLock.starLock.Show();
            }
        };
        onRemoving += () => transform.DOMoveY(transform.localPosition.y - 1, fadeTime);
    }
}