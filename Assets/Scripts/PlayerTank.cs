
using System.Collections;
using UnityEngine;

public class PlayerTank : Tank
{
    Vector3 TouchPoint=Vector3.zero;
    Camera main;
    Vector3 moveDirection;

    private void Start()
    {
        main = Camera.main;
    }
    protected override IEnumerator LookAt(Transform other)
    {
            while (Vector3.Angle(turret.forward, (other.position - transform.position)) > 5f)
            {
                turret.Rotate(turret.up, 4f);
                yield return null;
            }
        Fire();
    }

    protected override void Move()
    {
        ;
        float moveAxis = Input.GetAxis("Vertical");
        float rotAxis = Input.GetAxis("Horizontal");
        rb.MovePosition(transform.position + transform.forward * moveSpeed * moveAxis * Time.deltaTime);
        rb.MoveRotation(transform.rotation * Quaternion.Euler(transform.up * rotAxis * rotSpeed * Time.deltaTime));
        createMoveEffect(moveAxis);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LookAt(other));
        }
        if (Input.GetMouseButton(0))
        {
            SetTouchPoints();
        }
        GotoTouchPoint();

    }

    private void SetTouchPoints()
    {
        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        if(Physics.Raycast(ray,out RaycastHit info))
        {
            TouchPoint = new Vector3(info.point.x,transform.position.y,info.point.z);
            moveDirection = TouchPoint - transform.position;
        }
    }

    private void GotoTouchPoint()
    {
        transform.position = Vector3.Lerp(transform.position, TouchPoint, Time.deltaTime * moveSpeed);
        Quaternion lookRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
    }


}
