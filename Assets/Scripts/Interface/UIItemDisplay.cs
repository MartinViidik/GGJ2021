using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItemDisplay : MonoBehaviour
{
    public Image background;
    public Image icon;
    public TextMeshProUGUI text;

    public void UpdateDisplay(Item item)
    {
        icon.sprite = item.sprite;
        text.text = (item.usages).ToString();
    }
}
