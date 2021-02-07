using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableTooltip : MonoBehaviour
{
    public TextMeshProUGUI textField;

   public void SetTooltip(string message)
    {
        textField.text = message;
    }
}
