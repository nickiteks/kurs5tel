﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealsBar : MonoBehaviour
{
    [SerializeField]
    private Image healsImage;
    [SerializeField]
    private BaseStatsScript baseStats;

    private void Update()
    {
        healsImage.fillAmount = (baseStats.Health * 100) / baseStats.MaxHelth;
    }
}
