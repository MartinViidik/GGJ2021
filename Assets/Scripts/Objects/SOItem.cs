using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]
public class SOItem : ScriptableObject
{
    public string itemID;
    public Sprite sprite;
    public int usages;
}