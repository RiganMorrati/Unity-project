public class Level140ToolBoxShake : ShakePhone
{
    public Level140Hamer hamer;
    public Level140Crawbar crawbar;
    public TutorialStar wallPatchStar, stonePatchStar, chainStar;

    const float shakeTime = 0.7f;

    public override void Start()
    {
        onShakeComplete += () =>
        {
            active = false;
            GameManager.Instance.currentLocationController.Shake(shakeTime, 0.5f);
            hamer.MoveToStartPosition();
            crawbar.MoveToStartPosition();
            wallPatchStar.Show();
            stonePatchStar.Show();
            chainStar.Show();
        };
        base.Start();
    }
}