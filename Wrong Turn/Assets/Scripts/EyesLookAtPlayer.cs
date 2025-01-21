using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesLookAtPlayer : MonoBehaviour
{
    public Transform player;
    

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }
    }
}
