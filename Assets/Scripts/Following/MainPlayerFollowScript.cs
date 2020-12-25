using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainPlayerFollowScript : MonoBehaviour
{
    [SerializeField]
    private List<FollowPlayerScript> followers = new List<FollowPlayerScript>();

    [SerializeField]
    private float distanceCreatePoint;
    public float DistanceCreatePoint { get { return distanceCreatePoint; } }

    public void CreateFollowPoint(FollowPoint followPoint)
    {
        foreach (FollowPlayerScript follower in followers)
        {
            follower.AddFollowPoint(followPoint);
        }
    }
}
