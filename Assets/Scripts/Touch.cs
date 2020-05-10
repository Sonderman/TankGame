using UnityEngine;

public class Touch : Sense
{
    string tankName = "" ;
    public override void InitializeSense()
    {
        
    }

    public override void UpdateSense()
    {
        if(tankName == "playerTank")
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        tankName = other.name;
    }
    private void OnTriggerExit(Collider other)
    {
        tankName = "";
    }
}
