using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    //Function At Execution Start.
    void Awake()
    {
        //Get List of gameobjects With Tag of "Music" and add it to MusicObjects Array.
        GameObject[] MusicObjects = GameObject.FindGameObjectsWithTag("Music");

        //if MusicObjects size is more than 1.
        if (MusicObjects.Length > 1)
        {
            //Destroy this gameObject.
            Destroy(gameObject);
        }
        else
        {
            //Dont Destroy this gameObject when loading any scene.
            DontDestroyOnLoad(gameObject);
        }
    }
}
