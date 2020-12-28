using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFollow
{
    /// <summary>
    /// Необходимое текущее направление
    /// </summary>
    FollowPoint CurrentPoint { get; set; }
}
