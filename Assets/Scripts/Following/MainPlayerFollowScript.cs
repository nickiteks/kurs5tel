using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerFollowScript : MonoBehaviour
{
    [SerializeField]
    private List<FollowScript> followers = new List<FollowScript>();

    public void CreateFollowPoint(FollowPoint followPoint)
    {
        foreach (FollowScript follower in followers)
        {
            follower.AddFollowPoint(followPoint);
        }
    }
}
