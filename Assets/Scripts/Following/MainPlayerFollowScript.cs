using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainPlayerFollowScript : MonoBehaviour
{
    [SerializeField]
    private List<FollowScript> followers = new List<FollowScript>();

    [SerializeField]
    private float distanceCreatePoint;
    public float DistanceCreatePoint { get { return distanceCreatePoint; } }

    public void CreateFollowPoint(FollowPoint followPoint)
    {
        foreach (FollowScript follower in followers)
        {
            follower.AddFollowPoint(followPoint);
        }
    }
}
