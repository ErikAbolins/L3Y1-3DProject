using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseSequenceEnd : MonoBehaviour
{
    public ChaseSequence chaseSequence;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chaseSequence.ResetSpeed();
        }
    }
}
