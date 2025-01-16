using UnityEngine;

public class JewelCaseRotation : MonoBehaviour
{
    public float rotationSpeed = 10f;  // Speed of rotation

    void Update()
    {
        // Rotate the jewel case around its Y-axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
