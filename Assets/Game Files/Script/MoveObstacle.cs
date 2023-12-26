using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    //Integer for Obstcale Move Speed.
    public int MoveSpeed = 5;

    //Function Execute Every Frame.
    void Update()
    {
        //Move this gameobject transform to Left direction Multiplied with Move Speed at world calculation.
        transform.Translate((Vector2.left) * MoveSpeed * Time.deltaTime, Space.World);
    }
}