using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // GameObject Array of obstacle prefabs to spawn.
    public GameObject[] ObstcalesPrefaps;

    // Parent transform for organizing spawned obstacles.
    public Transform ObstcaleParent;

    // Obstacle height Variable.
    float height;
    // Obstacle rotation Variable.
    Quaternion ObstcaleRotation;

    //Function At Execution Start.
    void Start()
    {
        //Repeat Call Spawn Laser Method Start at 1s and repeat it self.
        InvokeRepeating("SpawnLaser", 1, 7);
        //Repeat Call Spawn Spike Method at 1s and repeat it self.
        InvokeRepeating("SpawnSpike", 1, 5);
        //Repeat Call Spawn Rocket Method at 1s and repeat it self.
        InvokeRepeating("SpawnRocket", 1, 3);
        //Repeat Call Spawn Coin Method at 1s and repeat it self.
        InvokeRepeating("SpawnCoin", 1, 2);

        //Repeat Call Spawn Laser Method Start at 60s and repeat it self.
        InvokeRepeating("SpawnLaser", 60, 12);
        //Repeat Call Spawn Spike Method Start at 60s and repeat it self.
        InvokeRepeating("SpawnSpike", 60, 7);
        //Repeat Call Spawn Rocket Method Start at 60s and repeat it self.
        InvokeRepeating("SpawnRocket", 60, 6);
    }

    // Method to spawn laser obstacles.
    void SpawnLaser()
    {
        // Set integer value for random value between (0-3).
        int val = Random.Range(0, 3);
        //if val is 0 then set height to 0, or val is 1 then set height to -2, otherways set height to 2.
        if (val == 0)
        {
            height = 0;
        }
        else if (val == 1)
        {
            height = -2;
        }
        else
        {
            height = 2;
        }

        // Set value for random value between (0-3).
        val = Random.Range(0, 3);
        //if val is 0 then set ObstcaleRotation to identity Quaternion, or val is 1 then set ObstcaleRotation to (0, 0, -45) Euler Quaternion, otherways set ObstcaleRotation to (0, 0, 45) Euler Quaternion.
        if (val == 0)
        {
            ObstcaleRotation = Quaternion.identity;
        }
        else if (val == 1)
        {
            ObstcaleRotation = Quaternion.Euler(0, 0, -45);
        }
        else
        {
            ObstcaleRotation = Quaternion.Euler(0, 0, 45);
        }

        // Instantiate the ObstcalesPrefaps[0] at ((x: 15),(y: height)) and rotation from ObstcaleRotation under ObstcaleParent.
        Instantiate(ObstcalesPrefaps[0], new Vector2(15, height), ObstcaleRotation, ObstcaleParent);
    }

    // Method to spawn spike obstacles
    void SpawnSpike()
    {
        // Set integer value for random value between (0-2).
        int val = Random.Range(0, 2);
        //if val is 0 then set height to (-4.062f) and ObstcaleRotation to identity Quaternion, otherways set height to (4.062f) and ObstcaleRotation to (0, 0, 180) Euler Quaternion.
        if (val == 0)
        {
            height = -4.062f;
            ObstcaleRotation = Quaternion.identity;
        }
        else
        {
            height = 4.062f;
            ObstcaleRotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }

        // Instantiate the ObstcalesPrefaps[2] at ((x: 15),(y: height)) and rotation from ObstcaleRotation under ObstcaleParent.
        Instantiate(ObstcalesPrefaps[2], new Vector2(15, height), ObstcaleRotation, ObstcaleParent);
    }

    // Method to spawn rocket obstacles
    void SpawnRocket()
    {
        // Set integer value for random value between (0-5).
        int val = Random.Range(0, 5);
        //if val is 0 then set height to 0, or val is 1 then set height to -2, or val is 2 then set height to 2, or val is 3 then set height to 3, otherways set height to -3.
        if (val == 0)
        {
            height = 0;
        }
        else if (val == 1)
        {
            height = -2;
        }
        else if (val == 2)
        {
            height = 2;
        }
        else if (val == 3)
        {
            height = 3;
        }
        else
        {
            height = -3;
        }

        // Set ObstcaleRotation to identity Quaternion.
        ObstcaleRotation = Quaternion.identity;

        // Instantiate the ObstcalesPrefaps[1] at ((x: 15),(y: height)) and rotation from ObstcaleRotation under ObstcaleParent.
        Instantiate(ObstcalesPrefaps[1], new Vector2(15, height), ObstcaleRotation, ObstcaleParent);
    }

    // Method to spawn coin obstacles
    void SpawnCoin()
    {
        // Set integer value for random value between (0-3).
        int val = Random.Range(0, 3);
        //if val is 0 then set height to 0, or val is 1 then set height to 2.5f, otherways set height to -2.5f.
        if (val == 0)
        {
            height = 0;
        }
        else if (val == 1)
        {
            height = 2.5f;
        }
        else
        {
            height = -2.5f;
        }

        // Set ObstcaleRotation to identity Quaternion.
        ObstcaleRotation = Quaternion.identity;

        // Instantiate the ObstcalesPrefaps[Randome num between (3-6)] at ((x: 15),(y: height)) and rotation from ObstcaleRotation under ObstcaleParent.
        Instantiate(ObstcalesPrefaps[Random.Range(3, 6)], new Vector2(15, height), ObstcaleRotation, ObstcaleParent);
    }
}