using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField]
    private Camera gameplayCamera;
    public Camera MainGameplayCamera { get { return gameplayCamera; } }

    [SerializeField]
    private Camera mapCamera;
    public Camera MapCamera { get { return mapCamera; } }
}
