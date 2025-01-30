using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseSequence : MonoBehaviour
{
    public CarController carController;
    public float chaseSpeed = 20f;
    public float normalSpeed;

    private void Start()
    {
        normalSpeed = carController.speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            carController.speed = chaseSpeed;
        }
    }

    public void ResetSpeed()
    {
        carController.speed = normalSpeed;
    }
}
