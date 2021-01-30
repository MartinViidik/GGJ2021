using UnityEngine;

public class Gate : MonoBehaviour
{
    public int sheepRescued;
    public int RescueTarget;

    public Sprite openGate;
    public SpriteRenderer gate;

    public Entity entity;
    // Start is called before the first frame update
    private void Start()
    {
        entity = GetComponent<Entity>();
        GameEvents.current.onTrigger += OnTrigger;
    }

    void OnTrigger(int interactableID, string triggerID)
    {
        if(triggerID == "cage")
        {
            sheepRescued++;
        }
        if(sheepRescued == RescueTarget)
        {
            gate.sprite = openGate;
        }
    }
}
