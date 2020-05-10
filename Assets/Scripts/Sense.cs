
using UnityEngine;

public abstract class Sense : MonoBehaviour , ISense
{
    public abstract void InitializeSense();

    public abstract void UpdateSense();
    

   
    void Start()
    {
        InitializeSense();
    }

    void Update()
    {
        UpdateSense();
    }
}
