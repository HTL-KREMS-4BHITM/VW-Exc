using UnityEngine;

public class RotateBehaviour : MonoBehaviour
{
    public Vector3 rotateVector;
    public float rotateSpeed;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateVector * rotateSpeed * Time.deltaTime);
    }
}
