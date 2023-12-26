using UnityEngine;

public class UsenInterfaceManager : MonoBehaviour
{
    //Function to Change Game Time Scale.
    public void SetGameTimeScale(int scale)
    {
        //Set Time Scale to scale value.
        Time.timeScale = scale;
    }
}