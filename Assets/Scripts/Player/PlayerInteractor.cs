using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public Entity entity;
    public Transform interactorAnchor;
  


    private void FixedUpdate()
    {
        float zAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        if (xAxis != 0 || zAxis != 0)
            interactorAnchor.rotation = Quaternion.LookRotation(new Vector3(xAxis, 0 , zAxis));
    }
}
