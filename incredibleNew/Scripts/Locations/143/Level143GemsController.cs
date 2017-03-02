using UnityEngine;

public class Level143GemsController : MonoBehaviour
{
    public LocationController controller;
    public Level143BucketController buckets;
    public Level143SeivingController seive;
    public Level143Gem[] gems;

    void Start()
    {
        for (int i = 0; i < gems.Length; i++)
            gems[i].onDeploy += LevelComplete;
    }

    void LevelComplete()
    {
        for (int i = 0; i < gems.Length; i++)
            if (!gems[i].IsDeployed)
            {
                seive.MoveSieveDown();
                buckets.EndSeiving();
                return;
            }
        controller.CompleteLevel();
    }
}