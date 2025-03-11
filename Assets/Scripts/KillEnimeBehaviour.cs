using UnityEngine;

public class KillEnimeBehaviour : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Robot"))   {
            Destroy(other.gameObject);
        }
    }
}
