using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    public Vector3 minSpeed;
    public Vector3 maxSpeed;
    Vector3 speed;
    public Vector3 minStart;
    public Vector3 maxStart;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(Vector3.Lerp(minStart, maxStart, UnityEngine.Random.Range(0f, 1f)));
        speed = Vector3.Lerp(minSpeed, maxSpeed, UnityEngine.Random.Range(0f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed);
    }
}
