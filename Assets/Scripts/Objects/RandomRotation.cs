using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    public bool stopped = false;
    public string triggerID = "ROTATEALL";
    public float maxTriggerDelay = 10f;
    public Vector3 minStart;
    public Vector3 maxStart;
    public Vector3 minSpeed;
    public Vector3 maxSpeed;
    Vector3 speed;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(Vector3.Lerp(minStart, maxStart, UnityEngine.Random.Range(0f, 1f)));
        speed = Vector3.Lerp(minSpeed, maxSpeed, UnityEngine.Random.Range(0f, 1f));
        GameEvents.current.onTrigger += StartRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopped)
            transform.Rotate(speed);
    }

    void StartRotation(int interactableID, string triggerID)
    {
        if (triggerID == this.triggerID)
            Invoke("RemoveRotationStop", UnityEngine.Random.Range(1f, Mathf.Max(2f, maxTriggerDelay)));
    }

    void RemoveRotationStop()
    {
        stopped = false;
    }
}
