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

        if (Input.GetButtonDown("Stun"))
        {
            Debug.Log("baaa");
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, 25, transform.forward, 1f);

            foreach (var hit in hits)
            {
                if (hit.transform.tag.Equals("Enemy"))
                {
                    Debug.Log("Enemy hit");
                    hit.transform.gameObject.GetComponent<EnemyAI>().SetState(EnemyAI.EnemyState.STUNNED);
                }
            }
        }
    }
}
