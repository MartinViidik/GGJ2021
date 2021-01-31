using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBahBar : MonoBehaviour
{
    public Image bahBar;

    void Start()
    {
        GameEvents.current.onPlayerBahUpdate += BahUpdate;
    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerBahUpdate -= BahUpdate;
    }

    void BahUpdate(float newBahPower)
    {
        bahBar.fillAmount = newBahPower;
    }
}
