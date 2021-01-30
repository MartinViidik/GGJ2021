using UnityEngine;

public class Cage : MonoBehaviour
{
    public GameObject Sheep;
    public GameObject Door;
    public Entity entity;
    public string triggerID;
    bool triggered = false;

    [SerializeField] Sprite OpenDoor;
    [SerializeField] Sprite ClosedDoor;

    public enum DoorState { LOCKED, OPEN };
    public DoorState ActiveState;

    private void Awake()
    {
        entity = GetComponent<Entity>();
    }

    private void Start()
    {
        SetDoorState(ActiveState);
        GameEvents.current.onTrigger += OnTrigger;
    }


    private void OnDestroy()
    {
        GameEvents.current.onTrigger -= OnTrigger;
    }

    void OnTrigger(int interactableID, string triggerID)
    {
        if (interactableID != 0 && interactableID != entity.entityID)
            return;

        if (triggered || triggerID != this.triggerID)
            return;

        SetDoorState(DoorState.OPEN);
    }

    public void SetDoorState(DoorState newState)
    {
        ActiveState = newState;
        switch (ActiveState)
        {
            case DoorState.LOCKED:
                {
                    Door.GetComponent<Collider>().enabled = true;
                    Door.GetComponent<SpriteRenderer>().sprite = ClosedDoor;
                    Sheep.GetComponent<EnemyAI>().ai.canMove = false;
                }
                break;
            case DoorState.OPEN:
                {
                    Door.GetComponent<Collider>().enabled = false;
                    Door.GetComponent<SpriteRenderer>().sprite = OpenDoor;
                    Sheep.GetComponent<EnemyAI>().ai.canMove = true;
                }
                break;
        }
    }
}
