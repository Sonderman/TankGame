
using UnityEngine;

public class Perspective : Sense
{
    float FieldOfView;
    float maxCheckDistance;
    public Transform other;
    private Animator fsm;
    public override void InitializeSense()
    {
        FieldOfView = 60;
        maxCheckDistance = 20;
        fsm = GetComponent<Animator>();
    }

    public override void UpdateSense()
    {

        Vector3 dir = (other.transform.position - transform.position).normalized; // Normalize mutlak değer almak
        Debug.DrawRay(transform.position + new Vector3(0, 2, 0), dir*2, Color.white);
        float angle = Vector3.Angle(dir, transform.forward);
        Debug.DrawRay(transform.position + new Vector3(0,2,0), transform.forward * maxCheckDistance, Color.blue);
        if (angle < FieldOfView)// Açı 30 dereceden küçük ise
        {
            Ray ray = new Ray(transform.position + new Vector3(0, 2, 0), dir * maxCheckDistance);
            
            if (Physics.Raycast(ray,out RaycastHit hitInfo, maxCheckDistance)) {
                Debug.DrawRay(transform.position + new Vector3(0, 2, 0), dir * maxCheckDistance, Color.green);
                string name = hitInfo.transform.name;
                Debug.Log("hitinfo:"+name);
                if (name.Equals("playerTank"))
                {
                    fsm.SetBool("visibility",true);
                }
                else
                {
                    fsm.SetBool("visibility", false);
                }
            }
            
        }
        else
        {
            fsm.SetBool("visibility", false);
        }
    }

    
}
