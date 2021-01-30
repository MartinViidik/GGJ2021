using UnityEngine;

public class Cage : MonoBehaviour
{
    public GameObject Sheep;
    public GameObject Door;

    [SerializeField] Sprite OpenDoor;
    [SerializeField] Sprite ClosedDoor;

    public enum DoorState { LOCKED, OPEN };
    public DoorState ActiveState;

    private void Start()
    {
        SetDoorState(ActiveState);
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
