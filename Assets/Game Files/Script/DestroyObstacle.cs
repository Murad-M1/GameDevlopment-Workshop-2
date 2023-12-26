using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    //Function to Detect Colliding.
    void OnTriggerEnter2D(Collider2D Object)
    {
        //Destroy Detected Object.
        Destroy(Object.gameObject);
    }
}