using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerFollowScript : MonoBehaviour
{
    [SerializeField]
    private List<FollowScript> followers = new List<FollowScript>();
    public GameObject gameObject1;
    public void CreateFollowPoint(FollowPoint followPoint)
    {
        Instantiate(gameObject1, followPoint.Position, Quaternion.identity);
        foreach (FollowScript follower in followers)
        {
            follower.AddFollowPoint(followPoint);
        }
    }
}
