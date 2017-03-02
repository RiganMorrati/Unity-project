public class Level140Chain : Level140Obstacle
{
    public Level140WoodPlank topPlank, bottomPlank;

    void Start()
    {
        isEnabled = true;
        onRemoving += star.Complete;
        onRemoved += () =>
        {
            topPlank.isEnabled = true;
            bottomPlank.isEnabled = true;
            topPlank.star.Show();
            bottomPlank.star.Show();
            star.Complete();
        };
    }
}