using UnityEngine;

public class Level143BucketController : MonoBehaviour
{
    public Level143Bucket[] buckets;
    public Level143SeivingController controller;

    void Start ()
    {
        for(int i = 0; i < buckets.Length; i++)
            buckets[i].onDrop += StartSeiving;
        controller.onSeived += EndSeiving;
    }
	
    void StartSeiving(Level143Bucket b)
    {
        for (int i = 0; i < buckets.Length; i++)
            buckets[i].SetMovable(false);
        controller.StartPouring(b);
    }
    
    public void EndSeiving()
    {
        for (int i = 0; i < buckets.Length; i++)
            buckets[i].SetMovable(true);
    }
}